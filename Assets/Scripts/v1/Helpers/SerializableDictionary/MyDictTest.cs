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


	//void Reset()
	//{
	//	// access by property
	//	//StringStringDictionary = new Dictionary<string, string>() { { "first key", "value A" }, { "second key", "value B" }, { "third key", "value C" } };
	//}
}
