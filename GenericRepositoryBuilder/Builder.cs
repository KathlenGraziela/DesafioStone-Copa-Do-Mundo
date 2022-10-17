using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace GenericRepositoryBuilder
{
    public partial class Builder
    {
        private readonly Type interfaceType;
        private readonly TypeBuilder typeBuilder;
        private readonly Type genericType;
        private readonly Type dbContextType;
        private readonly List<MethodInfo> interfaceMethods = new();
        private static readonly Dictionary<Type, object> Repositorys = new();
        private FieldBuilder? fbDbContext;

        private Builder(Type interfaceType, Type dbContextType)
        {
            this.interfaceType = interfaceType;
            genericType = ValidateInterface();
            InitializeMethodsIL();
            typeBuilder = CreateAsemblyModule();
            AddAndCheckMethodsImplementation();
            this.dbContextType = dbContextType;
        }
        public static void BuildRepository<TInterface, TDbContext>()
        {
            var repositoryInterfaceType = typeof(TInterface);
            if (Repositorys.ContainsKey(repositoryInterfaceType)) return;

            var repositoryBuild = new Builder(repositoryInterfaceType, typeof(TDbContext)).BuildType();
            Repositorys.Add(repositoryInterfaceType, repositoryBuild);
        }

        public static T GetRepository<T>(DbContext dbContext)
        {
            var repositoryInterfaceType = typeof(T);
            var repository = Repositorys[repositoryInterfaceType];
            repository.GetType().GetRuntimeFields().Single().SetValue(repository, dbContext);

            return (T)repository;
        }

        private TypeBuilder CreateAsemblyModule()
        {
            AssemblyName aName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);

            // The module name is usually the same as the assembly name.
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name);

            return mb.DefineType($"{interfaceType.Name}DynamicType", TypeAttributes.Public);
        }

        private Type ValidateInterface()
        {
            var genericInterface = interfaceType.GetInterface(typeof(IGenericRepository<>).Name);
            if (!interfaceType.IsInterface || genericInterface == null)
            {
                throw new Exception($"Type needs to be interface and generic");
            }
            return genericInterface.GenericTypeArguments.Single();
        }

        private void AddAndCheckMethodsImplementation()
        {   
            interfaceMethods.AddRange(interfaceType.GetInterfaces().SelectMany(i => i.GetMethods()));
            interfaceMethods.AddRange(interfaceType.GetMethods());

            var methNotImplemented = interfaceMethods.FirstOrDefault(m => !methodsIL.ContainsKey(m.Name));
            if (methNotImplemented != null)
                throw new Exception($"{methNotImplemented.Name} not implemented");
        }

        private object BuildType()
        {
            typeBuilder.AddInterfaceImplementation(interfaceType);
            fbDbContext = typeBuilder.DefineField("dbContext", typeof(DbContext), FieldAttributes.Private);

            GenerateConstructor();
            GenerateMethods();

            var tp = typeBuilder.CreateType() ?? throw new Exception();
            return Activator.CreateInstance(tp) ?? throw new Exception();
        }

        private void GenerateMethods()
        {
            foreach (var methInterface in interfaceMethods)
            {
                GenerateMethod(methInterface);
            }
        }

        private void GenerateMethod(MethodInfo methInterface)
        {
            var paramTypes = methInterface.GetParameters().Select(p => p.ParameterType).ToArray();
            var methBuilder = typeBuilder.DefineMethod
            (
                methInterface.Name,
                MethodAttributes.Public | MethodAttributes.Virtual,
                methInterface.ReturnType, paramTypes
            );

            typeBuilder.DefineMethodOverride(methBuilder, methInterface);

            ILGenerator iLGenerator = methBuilder.GetILGenerator();

            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldfld, fbDbContext);


            if(methInterface.Name != "SaveChangesAsync")
            {
                var dbSetType = GetDbSetGenericGetter(dbContextType);
                iLGenerator.Emit(OpCodes.Callvirt, dbSetType);
            }

            methodsIL[methInterface.Name].Invoke(iLGenerator);

            iLGenerator.Emit(OpCodes.Ret);
        }

        private void GenerateConstructor()
        {
            ConstructorBuilder constructor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

            ILGenerator iLGenerator = constructor.GetILGenerator();

            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));

            iLGenerator.Emit(OpCodes.Ret);
        }
    }
}
