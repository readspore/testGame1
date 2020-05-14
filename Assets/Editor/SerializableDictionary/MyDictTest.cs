using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDictTest : MonoBehaviour
{
	[SerializeField]
	StringStringDictionary m_stringStringDictionary = null;
	public IDictionary<string, string> StringStringDictionary
	{
		get { return m_stringStringDictionary; }
		set { m_stringStringDictionary.CopyFrom(value); }
	}


	[SerializeField]
	List<StringStringDictionary> ss = null;
	//public ObjectColorDictionary m_objectColorDictionary;

	//public StringStringArrayDictionary m_stringStringArrayDictionary;
	//public StringColorArrayDictionary m_stringColorArrayDictionary;
	//#if NET_4_6 || NET_STANDARD_2_0
	//	public StringHashSet m_stringHashSet;
	//#endif

	//void Reset()
	//{
	//	// access by property
	//	//StringStringDictionary = new Dictionary<string, string>() { { "first key", "value A" }, { "second key", "value B" }, { "third key", "value C" } };
	//}
}
