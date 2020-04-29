﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLvlAttrsSO : ScriptableObject, IItemAttrsSO
{
	[SerializeField]
	StringStringDictionary m_stringStringDictionary = null;
	public IDictionary<string, string> StringStringDictionary
	{
		get { return m_stringStringDictionary; }
		set { m_stringStringDictionary.CopyFrom(value); }
	}

	public string GetLvlAttr(string name)
    {
        throw new System.NotImplementedException();
    }

}