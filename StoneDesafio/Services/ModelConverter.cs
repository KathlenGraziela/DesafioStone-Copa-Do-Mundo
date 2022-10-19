using System.Collections;

namespace StoneDesafio.Models.Utils
{
    public class ModelConverter
    {
        public TOutput Convert<TOutput>(in object input, bool checkNull = false)
        {
            var output = (TOutput)Activator.CreateInstance(typeof(TOutput));
            ConvertInPlace(input, output, checkNull);
            return output;
        }

        public void ConvertInPlace(in object input, object output, bool checkNull = false)
        {
            foreach (var pI in input.GetType().GetProperties())
            {
                foreach (var pO in output.GetType().GetProperties())
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

        public TOutput DepthConvert<TOutput>(in object input)
        {
            var output = (TOutput)Activator.CreateInstance(typeof(TOutput));
            DepthConvertInPlace(input, output);
            return output;
        }

        public void DepthConvertInPlace(in object input, object output)
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
