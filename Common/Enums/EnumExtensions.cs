using Common.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;


namespace Common.Enums
{
    public static class EnumExtensions
    {
        public static void Add(this List<ResourceKey> enumNotification, Enum notiicationEnums)
        {

            var enumType = notiicationEnums.GetType().Name;
            var enumValue = notiicationEnums.ConvertToString();
            var finndEnum = enumNotification.Find(x => x.EnumName == enumType);
            if (finndEnum != null)
            {
                finndEnum.ResourceKeys.Add(enumValue);
            }
            else
            {
                var test = notiicationEnums.ToString();
                var test2 = (object)notiicationEnums.ToString();
                enumNotification.Add(new ResourceKey(enumType, enumValue));
            }

        }


        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum))
                    yield return (T)value;
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }

        public static string ConvertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }


    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }

	public enum DataTypes
	{
		UniqueIdentifier,
		Bit,
		Date,
		Datetime,
		Time,
		Nvarchar,
		Image,
		Real,
		Int,
		Binary,
		Varbinary,
		Tinyint,
		Varchar

	}

}
