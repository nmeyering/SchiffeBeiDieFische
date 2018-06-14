Shader "Custom/FuseShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_FuseRemaining ("FuseRemaining", Range(0,1)) = 0.0
		_Burning ("Burning", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade vertex:myvert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			
		};

		half _Glossiness;
		half _Metallic;
		half _FuseRemaining;
		half _Burning;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

        void myvert (inout appdata_full v, out Input data) {
              v.vertex.z *= _FuseRemaining;
              
              UNITY_INITIALIZE_OUTPUT(Input,data);
              
//              float pos = length(UnityObjectToViewPos(v.vertex).xyz);
//              float diff = unity_FogEnd.x - unity_FogStart.x;
//              float invDiff = 1.0f / diff;
//              data.fog = clamp ((unity_FogEnd.x - pos) * invDiff, 0.0, 1.0);
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			// o.Albedo = c.rgb;
			o.Emission = _Burning * lerp(
                    fixed3(0, 0, 0),
                    15 * fixed3(1, 0.7, 0.1),
                    (pow(IN.uv_MainTex.x, 10) * (_FuseRemaining))
            );
//			o.Albedo = fixed3(1,0,1);//fixed4(IN.uv_MainTex.x - sin(_Time.z), 0, 0, 1);
			// Metallic and smoothness come from slider variables
			o.Metallic = 0;//_Metallic;
			o.Smoothness = 0;//_Glossiness;
			// o.Alpha = c.a;
			// o.Alpha = smoothstep( sin(_Time.z + 5), sin(_Time.z + 5) + 0.05, 1 - IN.uv_MainTex.x);// * sin(_Time.z);
		o.Alpha = 1;
			// o.Alpha = 1 - step(_FuseRemaining, IN.uv_MainTex.x);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
