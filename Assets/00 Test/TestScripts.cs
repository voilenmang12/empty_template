using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Sirenix.OdinInspector;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;

public class TestScripts : SerializedMonoBehaviour
{
    public CustomBigValue a;
    [Button()]
    void TestCustomBigValue(string _operator, string value)
    {
        switch (_operator)
        {
            case "+":
                a += new CustomBigValue(value);
                break;
            case "-":
                a -= new CustomBigValue(value);
                break;
            case "*":
                a *= new CustomBigValue(value);
                break;
            case "/":
                a /= new CustomBigValue(value);
                break;
            case "^":
                a ^= int.Parse(value);
                break;
            default:
                break;
        }
    }
    public string jsonClassTest;
    public TestClass testClass;
    [Button()]
    void Test()
    {
        testClass = new TestClass();
        testClass.value = new byte[10000];
        for (int i = 0; i < testClass.value.Length; i++)
        {
            testClass.value[i] = (byte) Random.Range(0,2);
        }
        jsonClassTest = Newtonsoft.Json.JsonConvert.SerializeObject(testClass);
        testClass = Newtonsoft.Json.JsonConvert.DeserializeObject<TestClass>(jsonClassTest);
    }
}
public class TestClass
{
    public byte[] value;
}