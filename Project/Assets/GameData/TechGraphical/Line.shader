// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33069,y:32425,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32631,y:32501,varname:node_2393,prsc:2|A-4150-OUT,B-2053-RGB,C-8502-OUT,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31649,y:33241,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.01648069,c3:0.1838235,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2dAsset,id:5085,x:31234,y:32331,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5085,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0932b2c253fb7004f992ba4f2ceb1d75,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7156,x:30968,y:32625,varname:node_7156,prsc:2,tex:0932b2c253fb7004f992ba4f2ceb1d75,ntxv:0,isnm:False|TEX-5085-TEX;n:type:ShaderForge.SFN_Tex2d,id:8688,x:31716,y:32605,varname:node_8688,prsc:2,tex:0932b2c253fb7004f992ba4f2ceb1d75,ntxv:0,isnm:False|UVIN-6517-OUT,TEX-5085-TEX;n:type:ShaderForge.SFN_TexCoord,id:7602,x:31363,y:33029,varname:node_7602,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7489,x:31202,y:32869,varname:node_7489,prsc:2|A-9551-OUT,B-5956-OUT,C-7156-G;n:type:ShaderForge.SFN_ComponentMask,id:6208,x:31374,y:32842,varname:node_6208,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7489-OUT;n:type:ShaderForge.SFN_Add,id:6517,x:31673,y:32843,varname:node_6517,prsc:2|A-6208-OUT,B-7602-UVOUT;n:type:ShaderForge.SFN_Slider,id:9551,x:30856,y:33006,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_9551,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1273548,max:1;n:type:ShaderForge.SFN_Time,id:3831,x:29820,y:32756,varname:node_3831,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4150,x:32050,y:32731,varname:node_4150,prsc:2|A-8688-R,B-8688-G;n:type:ShaderForge.SFN_ScreenPos,id:1810,x:29820,y:32589,varname:node_1810,prsc:2,sctp:2;n:type:ShaderForge.SFN_Noise,id:9151,x:30422,y:32548,varname:node_9151,prsc:2|XY-90-OUT;n:type:ShaderForge.SFN_Noise,id:6070,x:30422,y:32671,varname:node_6070,prsc:2|XY-1810-UVOUT;n:type:ShaderForge.SFN_Append,id:5549,x:30623,y:32654,varname:node_5549,prsc:2|A-9151-OUT,B-6070-OUT;n:type:ShaderForge.SFN_Add,id:7462,x:30057,y:32648,varname:node_7462,prsc:2|A-1810-UVOUT,B-3831-TSL;n:type:ShaderForge.SFN_Multiply,id:90,x:30256,y:32514,varname:node_90,prsc:2|A-9412-OUT,B-7462-OUT;n:type:ShaderForge.SFN_Vector1,id:9412,x:30085,y:32502,varname:node_9412,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Subtract,id:8364,x:30742,y:32809,varname:node_8364,prsc:2|A-2711-RGB,B-6302-OUT;n:type:ShaderForge.SFN_Vector1,id:6302,x:30484,y:32879,varname:node_6302,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Normalize,id:5956,x:30924,y:32809,varname:node_5956,prsc:2|IN-8364-OUT;n:type:ShaderForge.SFN_Tex2d,id:2711,x:30590,y:33040,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_2711,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d9bef911806336343804962d346382cd,ntxv:0,isnm:False|UVIN-6246-OUT;n:type:ShaderForge.SFN_ScreenPos,id:5023,x:29661,y:33074,varname:node_5023,prsc:2,sctp:2;n:type:ShaderForge.SFN_Add,id:6246,x:30386,y:33040,varname:node_6246,prsc:2|A-1836-OUT,B-6805-OUT;n:type:ShaderForge.SFN_Time,id:807,x:29949,y:33304,varname:node_807,prsc:2;n:type:ShaderForge.SFN_ScreenParameters,id:5694,x:29251,y:33320,varname:node_5694,prsc:2;n:type:ShaderForge.SFN_Append,id:2326,x:29470,y:33288,varname:node_2326,prsc:2|A-5694-PXW,B-5694-PXH;n:type:ShaderForge.SFN_Divide,id:33,x:29661,y:33257,varname:node_33,prsc:2|A-2326-OUT,B-1518-OUT;n:type:ShaderForge.SFN_Vector1,id:1518,x:29470,y:33453,varname:node_1518,prsc:2,v1:256;n:type:ShaderForge.SFN_Multiply,id:1836,x:29949,y:33154,varname:node_1836,prsc:2|A-5023-UVOUT,B-33-OUT;n:type:ShaderForge.SFN_Lerp,id:8502,x:31970,y:33113,varname:node_8502,prsc:2|A-4397-RGB,B-797-RGB,T-7156-G;n:type:ShaderForge.SFN_Tex2d,id:4397,x:30590,y:33271,ptovrint:False,ptlb:Color Noise,ptin:_ColorNoise,varname:node_4397,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6a52099545c533244841bdf5ee3f9d16,ntxv:2,isnm:False|UVIN-301-OUT;n:type:ShaderForge.SFN_Add,id:301,x:30396,y:33321,varname:node_301,prsc:2|A-1836-OUT,B-5107-OUT;n:type:ShaderForge.SFN_Slider,id:4683,x:29780,y:33008,ptovrint:False,ptlb:Noise Speed,ptin:_NoiseSpeed,varname:node_4683,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:7974,x:29804,y:33494,ptovrint:False,ptlb:ColorNoise Speed,ptin:_ColorNoiseSpeed,varname:node_7974,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6805,x:30202,y:33073,varname:node_6805,prsc:2|A-807-TTR,B-4683-OUT;n:type:ShaderForge.SFN_Multiply,id:5107,x:30205,y:33353,varname:node_5107,prsc:2|A-807-TTR,B-7974-OUT;proporder:797-5085-9551-2711-4397-4683-7974;pass:END;sub:END;*/

Shader "Proxor/Line" {
    Properties {
        _TintColor ("Color", Color) = (0,0.01648069,0.1838235,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _Power ("Power", Range(0, 1)) = 0.1273548
        _Noise ("Noise", 2D) = "white" {}
        _ColorNoise ("Color Noise", 2D) = "black" {}
        _NoiseSpeed ("Noise Speed", Range(0, 1)) = 0
        _ColorNoiseSpeed ("ColorNoise Speed", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Power;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform sampler2D _ColorNoise; uniform float4 _ColorNoise_ST;
            uniform float _NoiseSpeed;
            uniform float _ColorNoiseSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
////// Lighting:
////// Emissive:
                float2 node_1836 = (sceneUVs.rg*(float2(_ScreenParams.r,_ScreenParams.g)/256.0));
                float4 node_807 = _Time + _TimeEditor;
                float2 node_6246 = (node_1836+(node_807.a*_NoiseSpeed));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_6246, _Noise));
                float4 node_7156 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_6517 = ((_Power*normalize((_Noise_var.rgb-0.5))*node_7156.g).rg+i.uv0);
                float4 node_8688 = tex2D(_MainTex,TRANSFORM_TEX(node_6517, _MainTex));
                float2 node_301 = (node_1836+(node_807.a*_ColorNoiseSpeed));
                float4 _ColorNoise_var = tex2D(_ColorNoise,TRANSFORM_TEX(node_301, _ColorNoise));
                float3 emissive = ((node_8688.r*node_8688.g)*i.vertexColor.rgb*lerp(_ColorNoise_var.rgb,_TintColor.rgb,node_7156.g)*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA*1.5f;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
