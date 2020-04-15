using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn 
{
    int id;
    Vector3 position = Vector3.zero;
    string name;
    public Respawn(int id)
    {
        string respInfo = PlayerPrefs.GetString("respawn" + id);
        string delimeter = "|";
        int count = 0;
        while (count < 4)
        {
            int di1 = respInfo.IndexOf(delimeter);
            if (di1 != -1)
            {
                var c1 = respInfo.Substring(0, di1); 

                switch (count)
                {
                    case 0:
                        id = int.Parse(c1);
                        break;
                    case 1:
                        position.x = int.Parse(c1);
                        break;
                    case 2:
                        position.y = int.Parse(c1);
                        break;
                }
                respInfo = respInfo.Remove(0, c1.Length + 1); // +1 - delimeter
            }
            else if (count == 3)
            {
                name = respInfo;
            }
            ++count;
        }
    }

    public void MoveToRespawn()
    {
        GameObject.Find("Player").transform.position = this.position;
    }
}
