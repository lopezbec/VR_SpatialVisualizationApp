// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Fade180"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MainTex("MainTex", 2D) = "white" {}
		_Vector1("Vector 1", Vector) = (0,0,0,0)
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTex;
		uniform float2 _Vector0;
		uniform float2 _Vector1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = tex2D( _MainTex, ( ( _Vector0 * i.uv_texcoord ) + _Vector1 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=12001
71;480;1470;513;1237;187.5;1;True;True
Node;AmplifyShaderEditor.Vector2Node;3;-603,-123.5;Float;False;Property;_Vector0;Vector 0;1;0;0,0;0;3;FLOAT2;FLOAT;FLOAT
Node;AmplifyShaderEditor.TexCoordVertexDataNode;7;-736,117.5;Float;False;0;2;0;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector2Node;8;-511,154.5;Float;False;Property;_Vector1;Vector 1;1;0;0,0;0;3;FLOAT2;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-348,-121.5;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-285,49.5;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;1;-94,-133.5;Float;True;Property;_MainTex;MainTex;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;263,-141;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;Fade180;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;4;1;7;0
WireConnection;6;0;4;0
WireConnection;6;1;8;0
WireConnection;1;1;6;0
WireConnection;0;2;1;0
ASEEND*/
//CHKSM=9F29520150588949A816969E2E21DDA7E56350A4