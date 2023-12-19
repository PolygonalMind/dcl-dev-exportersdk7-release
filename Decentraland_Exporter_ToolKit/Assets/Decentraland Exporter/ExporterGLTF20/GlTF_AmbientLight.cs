#if UNITY_EDITOR

public class GlTF_AmbientLight : GlTF_Light {
	public override void Write()
	{
		color.Write();
	}
}
#endif