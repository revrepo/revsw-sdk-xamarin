using System;

namespace Com.Nuubit.Sdk.Config.Serialization
{
	partial class ConfigListDeserialize : global::Java.Lang.Object, global::GoogleGson.IJsonDeserializer
	{

		unsafe Java.Lang.Object global::GoogleGson.IJsonDeserializer.Deserialize(global::GoogleGson.JsonElement p0, global::Java.Lang.Reflect.IType p1, global::GoogleGson.IJsonDeserializationContext p2)
		{
			return this.Deserialize(p0, p1, p2);
		}

	
	}


	partial class ConfigListSerialize : global::Java.Lang.Object, global::GoogleGson.IJsonSerializer
	{

		unsafe global::GoogleGson.JsonElement global::GoogleGson.IJsonSerializer.Serialize(global::Java.Lang.Object p0, global::Java.Lang.Reflect.IType p1, global::GoogleGson.IJsonSerializationContext p2)
		{
			return this.Serialize((global::Com.Nuubit.Sdk.Config.ConfigsList)p0, p1, p2);
		}
	}


	 partial class ConfigParametersDeserialize : global::Java.Lang.Object, global::GoogleGson.IJsonDeserializer
	{
		 unsafe global::Java.Lang.Object global::GoogleGson.IJsonDeserializer.Deserialize(global::GoogleGson.JsonElement p0, global::Java.Lang.Reflect.IType p1, global::GoogleGson.IJsonDeserializationContext p2)
		{
			return this.Deserialize(p0, p1, p2);
		}

	}
}
