#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DCLExport;

public class GlTF_Material : GlTF_Writer {

	public class Value : GlTF_Writer {
	}

	public class ColorValue : Value {
		public Color color;
		public bool isRGB = false;

		public override void Write()
		{
			jsonWriter.Write ("\"" + name + "\": [");
			jsonWriter.Write (SceneTraverserUtils.FloatToString(color.r) + ", " + SceneTraverserUtils.FloatToString(color.g) + ", " + SceneTraverserUtils.FloatToString(color.b) + (isRGB ? "" : ", " + SceneTraverserUtils.FloatToString(color.a)));
			jsonWriter.Write ("]");
		}
	}

	public class VectorValue : Value {
		public Vector4 vector;

		public override void Write()
		{
			jsonWriter.Write ("\"" + name + "\": [");
			jsonWriter.Write (SceneTraverserUtils.FloatToString(vector.x) + ", " + SceneTraverserUtils.FloatToString(vector.y) + ", " + SceneTraverserUtils.FloatToString(vector.z) + ", " + SceneTraverserUtils.FloatToString(vector.w));
			jsonWriter.Write ("]");
		}
	}

	public class FloatValue : Value {
		public float value;

		public override void Write()
		{
			jsonWriter.Write("\"" + name + "\": " + SceneTraverserUtils.FloatToString(value));

        }
	}

	public class IntValue : Value
	{
		public int value;

		public override void Write()
		{
			jsonWriter.Write("\"" + name + "\": " + value);
		}
	}

	public class BoolValue : Value
	{
		public bool value;

		public override void Write()
		{
			jsonWriter.Write("\"" + name + "\": " + (value ? "true" : "false"));
		}
	}

	public class StringValue : Value {
		public string value;

		public override void Write()
		{
			jsonWriter.Write ("\"" + name + "\": \"" + value +"\"");
		}
	}

	public class DictValue: Value
	{
		public Dictionary<string, int> intValue;
		public Dictionary<string, float> floatValue;
		public Dictionary<string, string> stringValue;
		public DictValue()
		{
			intValue = new Dictionary<string, int>();
			floatValue = new Dictionary<string, float>();
			stringValue = new Dictionary<string, string>();
		}
		public override void Write()
		{
			jsonWriter.Write("\"" + name + "\" : {\n");
			IndentIn();

			foreach (string key in intValue.Keys)
			{
				CommaNL();
				Indent(); jsonWriter.Write("\"" + key + "\" : " + intValue[key]);
			}
			foreach (string key in floatValue.Keys)
			{
				CommaNL();
				Indent(); jsonWriter.Write("\"" + key + "\" : " + SceneTraverserUtils.FloatToString(floatValue[key]));
			}
			foreach (string key in stringValue.Keys)
			{
				CommaNL();
				Indent(); jsonWriter.Write("\"" + key + "\" : " + stringValue[key]);
			}
			jsonWriter.Write("\n");
			IndentOut();
			Indent(); jsonWriter.Write("}");
		}
	}

	public int instanceTechniqueIndex;
	public bool isMetal = false;
	public float shininess;
	public List<Value> values = new List<Value>();
	public List<Value> pbrValues = new List<Value>();

	public static string GetNameFromObject(Object o)
	{
		return "material_" + GetNameFromObject(o, true);
	}

	public override void Write()
	{
		Indent(); jsonWriter.Write("{\n");
		IndentIn();
		writeExtras();
		if (isMetal)
		{
			Indent(); jsonWriter.Write("\"pbrMetallicRoughness\": {\n");
		}
		else
		{
			Indent(); jsonWriter.Write("\"extensions\": {\n");
			IndentIn();

			Indent(); jsonWriter.Write("\"KHR_materials_pbrSpecularGlossiness\": {\n");
		}
		IndentIn();
		foreach (var v in pbrValues)
		{
			CommaNL();
			Indent(); v.Write();
		}
		if (!isMetal)
		{
			IndentOut();
			Indent(); jsonWriter.Write("}");
			jsonWriter.Write("\n");
		}

		jsonWriter.Write("\n");
		IndentOut();
		Indent(); jsonWriter.Write("},\n");

		// write common values
		foreach (var v in values)
		{
			CommaNL();
			Indent(); v.Write();
		}
		CommaNL();
		Indent();		jsonWriter.Write ("\"name\": \"" + name + "\"\n");
		IndentOut();
		Indent();		jsonWriter.Write ("}");

	}

}
#endif