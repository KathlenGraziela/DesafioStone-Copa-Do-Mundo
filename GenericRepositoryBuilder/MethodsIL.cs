using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GenericRepositoryBuilder
{
    public partial class Builder
    {
        private readonly Dictionary<string, Action<ILGenerator>> methodsIL = new();

        private void InitializeMethodsIL()
        {            
            methodsIL.Add("SelectAllAsync", (il) => ToListAsyncIL(il));

            Action<ILGenerator> Where = (il) =>
            {
                GenericQueriableIL(il, nameof(Queryable.Where));
                ToListAsyncIL(il);
            };
            methodsIL.Add("SelectWhereAsync", Where);

            Action<ILGenerator> Take = (il) =>
            {
                GenericQueriableIL(il, nameof(Queryable.Take));
                ToListAsyncIL(il);
            };
            methodsIL.Add("SelectNAsync", Take);

            Action<ILGenerator> FirstOrDefault = (il) =>
            {
                FirstOrDefaultAsyncIL(il);
            };
            methodsIL.Add("FindFirstAsync", FirstOrDefault);

            Action<ILGenerator> Update = (il) =>
            {
                GenericDbSetIL(il, nameof(DbSet<object>.Update));
                il.Emit(OpCodes.Pop);
            };
            methodsIL.Add("Update", Update);

            Action<ILGenerator> Remove = (il) =>
            {
                GenericDbSetIL(il, nameof(DbSet<object>.Remove));
                il.Emit(OpCodes.Pop);
            };
            methodsIL.Add("Remove", Remove);

            Action<ILGenerator> SaveChangesAsync = (il) =>
            {
                SaveChangesAsyncIL(il);
            };
            methodsIL.Add("SaveChangesAsync", SaveChangesAsync);

            Action<ILGenerator> Add = (il) =>
            {
                GenericDbSetIL(il, nameof(DbSet<object>.Add));
                il.Emit(OpCodes.Pop);
            };
            methodsIL.Add("Add", Add);

            Action<ILGenerator> AddSave = (il) =>
            {
                Add(il);
                LoadDbContextIL(il);
                SaveChangesAsyncIL(il);
            };
            methodsIL.Add("AddAndSaveAsync", AddSave);

            Action<ILGenerator> UpdateSave = (il) =>
            {
                Update(il);
                LoadDbContextIL(il);
                SaveChangesAsyncIL(il);
            };
            methodsIL.Add("UpdateAndSaveAsync", UpdateSave);


            Action<ILGenerator> RemoveSave = (il) =>
            {
                Remove(il);
                LoadDbContextIL(il);
                SaveChangesAsyncIL(il);
            };
            methodsIL.Add("RemoveAndSaveAsync", RemoveSave);

            Action<ILGenerator> FindAsync = (il) =>
            {
                GenericDbSetIL(il, nameof(DbSet<object>.FindAsync));
            };
            methodsIL.Add("FindAsync", FindAsync);
        }

        private void GenericQueriableIL(ILGenerator iLGenerator, string methName)
        {
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Call, GetMethod(typeof(Queryable), methName));
        }

        private void FirstOrDefaultAsyncIL(ILGenerator iLGenerator)
        {
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Call, GetCancellationTokenGetter());
            iLGenerator.Emit(OpCodes.Call, GetFirstOrDefaultAsyncMethod());
        }

        private void ToListAsyncIL(ILGenerator iLGenerator)
        {
            iLGenerator.Emit(OpCodes.Call, GetCancellationTokenGetter());
            iLGenerator.Emit(OpCodes.Call, GetToListAsyncMethod());
        }
        private void SaveChangesAsyncIL(ILGenerator iLGenerator)
        {
            iLGenerator.Emit(OpCodes.Call, GetCancellationTokenGetter());
            iLGenerator.Emit(OpCodes.Call, GetSaveChangesAsyncMethod());
        }

        private void GenericDbContextIL(ILGenerator iLGenerator, string methName)
        {
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Call, GetCancellationTokenGetter());
            iLGenerator.Emit(OpCodes.Callvirt, GetMethod(typeof(DbContext), methName));
        }


        private void GenericDbSetIL(ILGenerator iLGenerator, string methName)
        {
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Callvirt, GetMethod(typeof(DbSet<>).MakeGenericType(genericType), methName));
        }

        private void LoadDbContextIL(ILGenerator iLGenerator)
        {
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldfld, fbDbContext);
        }
    }
}
