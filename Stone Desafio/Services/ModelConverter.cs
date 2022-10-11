using System.Collections;

namespace Stone_Desafio.Models.Utils
{
    public class ModelConverter
    {
        public TOutput Convert<TOutput, TInput>(in TInput input, bool checkNull = false)
        {
            var output = (TOutput?)Activator.CreateInstance(typeof(TOutput));
            ConvertInPlace(input, output, checkNull);
            return output;
        }

        public void ConvertInPlace<TOutput, TInput>(in TInput input, TOutput output, bool checkNull = false)
        {
            foreach (var pI in typeof(TInput).GetProperties())
            {
                foreach (var pO in typeof(TOutput).GetProperties())
                {
                    if (checkNull && pI.GetValue(input) == null)
                    {
                        break;
                    }

                    if (pI.Name == pO.Name && pI.PropertyType == pO.PropertyType)
                    {
                        pO.SetValue(output, pI.GetValue(input));
                        break;
                    }
                }
            }
        }

        public TOutput DepthConvert<TOutput, TInput>(in TInput input)
        {
            var output = (TOutput?)Activator.CreateInstance(typeof(TOutput));
            DepthConvertInPlace(input, output);
            return output;
        }

        public void DepthConvertInPlace<TOutput, TInput>(in TInput input, TOutput output)
        {
            foreach (var pI in input.GetType().GetProperties())
            {
                foreach (var pO in output.GetType().GetProperties())
                {

                    if (pI.Name == pO.Name)
                    {
                        if(pI.PropertyType == pO.PropertyType)
                        {
                            pO.SetValue(output, pI.GetValue(input));
                            break;
                        }
                        else if (pI.PropertyType.Name == typeof(List<>).Name && pO.PropertyType.Name == typeof(List<>).Name)
                        {
                            var propInputList = (IEnumerable<object>) pI.GetValue(input);
                            if(propInputList == null) break;
                            
                            var propListObjectType = pO.PropertyType.GenericTypeArguments.Single();

                            var propOutputList = (IList?) Activator.CreateInstance(pO.PropertyType);

                            foreach (var obj in propInputList)
                            {
                                var objOut = Activator.CreateInstance(propListObjectType);

                                DepthConvertInPlace(obj, objOut);

                                propOutputList?.Add(objOut);
                            }

                            pO.SetValue(output, propOutputList);
                        }
                        else if (!pI.PropertyType.IsPrimitive && !pO.PropertyType.IsPrimitive)
                        {
                            var propOutput = pO.GetValue(output) ?? Activator.CreateInstance(pO.PropertyType);

                            DepthConvertInPlace(pI.GetValue(input), propOutput);

                            pO.SetValue(output, propOutput);
                        }
                    }
                }
            }
        }
    }
}
