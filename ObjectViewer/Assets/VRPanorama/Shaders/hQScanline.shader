// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "hQScanline"
{
	Properties
	{
		_Main("Main", 2D) = "white" {}
		_Float0("Float 0", Float) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform sampler2D _Main;
			uniform float hQSteps;
			uniform float scanlineSoft;
			uniform float hQStep;
			uniform float _Float0;
			uniform float sbs;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord = screenPos;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float4 screenPos = i.ase_texcoord;
				float4 ase_screenPosNorm = screenPos/screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult24 = (float2(ase_screenPosNorm.x , ase_screenPosNorm.y));
				float temp_output_25_0 = ( 1.0 / hQSteps );
				float temp_output_36_0 = ( sbs * 4.0 );
				float smoothstepResult8 = smoothstep( temp_output_25_0 , ( temp_output_25_0 + ( scanlineSoft / hQSteps ) ) , ( abs( ( frac( ( ( ase_screenPosNorm.x + ( ( ( ( ( ( hQStep + _Float0 ) * 10.0 ) * temp_output_25_0 ) - 5.0 ) * 0.1 ) / temp_output_36_0 ) ) * temp_output_36_0 ) ) - 0.5 ) ) * 2.0 ));
				float4 appendResult41 = (float4((tex2D( _Main, appendResult24 )).rgb , ( 1.0 - smoothstepResult8 )));
				
				
				finalColor = appendResult41;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15406
327.3333;662;1667;577;2709.136;1188.578;3.030259;True;True
Node;AmplifyShaderEditor.RangedFloatNode;15;-2203.403,-973.3986;Float;False;Global;hQStep;hQStep;0;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-2215.091,-636.7968;Float;False;Property;_Float0;Float 0;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1036.401,-629.1;Float;False;Global;hQSteps;hQSteps;1;0;Create;True;0;0;False;0;9;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-1910.591,-704.7969;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;25;-636.5974,-424.8995;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1747.191,-621.0995;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1517.99,-552.2992;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-1100.388,389.2015;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;False;0;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-1135.789,275.4024;Float;False;Global;sbs;sbs;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;18;-1381.901,-417.4001;Float;False;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-840.3882,270.9015;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1304.001,-263.5001;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-1112.301,-240.7001;Float;False;2;0;FLOAT;0;False;1;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;4;-1545.2,95.89973;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-1190.802,93.79994;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-840.0002,111.1994;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;6;-636.3995,76.69967;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-493.2992,76.09973;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-777.5975,-227.7992;Float;False;Global;scanlineSoft;scanlineSoft;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;11;-329.2004,79.89971;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;-564.4885,-92.69714;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;24;-438.5969,-784.4994;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-352.6009,-124.8002;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.03;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-166.8006,79.89969;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-21.9962,-604.0991;Float;True;Property;_Main;Main;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;8;15.89986,-99.50012;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;40;506.33,-507.0872;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;19;257.3005,-170.4003;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-2012.687,-886.6981;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;26;-832.894,-741.6002;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;41;769.8813,-500.1569;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;38;1025.526,-502.1065;Float;False;True;2;Float;ASEMaterialInspector;0;1;hQScanline;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;32;0;15;0
WireConnection;32;1;33;0
WireConnection;25;1;9;0
WireConnection;28;0;32;0
WireConnection;27;0;28;0
WireConnection;27;1;25;0
WireConnection;18;0;27;0
WireConnection;36;0;34;0
WireConnection;36;1;35;0
WireConnection;17;0;18;0
WireConnection;16;0;17;0
WireConnection;16;1;36;0
WireConnection;14;0;4;1
WireConnection;14;1;16;0
WireConnection;5;0;14;0
WireConnection;5;1;36;0
WireConnection;6;0;5;0
WireConnection;10;0;6;0
WireConnection;11;0;10;0
WireConnection;30;0;21;0
WireConnection;30;1;9;0
WireConnection;24;0;4;1
WireConnection;24;1;4;2
WireConnection;13;0;25;0
WireConnection;13;1;30;0
WireConnection;12;0;11;0
WireConnection;23;1;24;0
WireConnection;8;0;12;0
WireConnection;8;1;25;0
WireConnection;8;2;13;0
WireConnection;40;0;23;0
WireConnection;19;0;8;0
WireConnection;37;0;15;0
WireConnection;37;1;34;0
WireConnection;26;1;9;0
WireConnection;41;0;40;0
WireConnection;41;3;19;0
WireConnection;38;0;41;0
ASEEND*/
//CHKSM=C198AEFA1668377944A52D9D02751CC165FC963A