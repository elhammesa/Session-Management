using Common.Enums;


namespace Common.Helper;

    public class ResourceKeyResult
    {
        public ResultType ResultType { get; set; }
        public List<ResourceKey> ResourceKeyList { get; set; }


        public ResourceKeyResult(ResultType resultType = ResultType.Warning, Enum enumValue = null)
        {


            ResultType = resultType;
            ResourceKeyList = ResourceKeyList ?? new List<ResourceKey>();

            if (enumValue != null)
            {
                ResourceKey resultResourceKey = new ResourceKey(enumValue.GetType().Name, enumValue.ToString());
                ResourceKeyList.Add(resultResourceKey);
            }


        }
    }

