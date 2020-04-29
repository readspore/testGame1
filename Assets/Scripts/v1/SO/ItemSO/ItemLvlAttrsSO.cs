using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAttrType
{
	Kd,
	TimeApplayed,
	SilverCost,
	GoldCost,
	Arm,
	TimeScale
}; 

[CreateAssetMenu]
public class ItemLvlAttrsSO : ScriptableObject, IItemAttrsSO
{
	[SerializeField]
	[Header("Проверь правильность введеных тобою значений!")]
	StringStringDictionary m_stringStringDictionary = null;
	public IDictionary<string, string> StringStringDictionary
	{
		get { return m_stringStringDictionary; }
		set { m_stringStringDictionary.CopyFrom(value); }
	}

	public string GetLvlAttr(ItemAttrType attrnName)
    {
		string result;
		if (StringStringDictionary.TryGetValue(attrnName.ToString(), out result))
		{
			return result;
		}
		return "null";
	}

}
