// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:14,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34509,y:32219,varname:node_3138,prsc:2|emission-3018-OUT,custl-7741-OUT;n:type:ShaderForge.SFN_Tex2d,id:4934,x:33529,y:32758,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:_MainTexture,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:197d190f19ce06e48831c7afbfdeef7c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_LightVector,id:4598,x:32320,y:32380,varname:node_4598,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1741,x:32320,y:32202,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:4493,x:32623,y:32282,varname:node_4493,prsc:2,dt:0|A-1741-OUT,B-4598-OUT;n:type:ShaderForge.SFN_Max,id:7397,x:33198,y:32426,varname:rampMax,prsc:0|A-2787-OUT,B-7851-OUT;n:type:ShaderForge.SFN_Add,id:2787,x:33005,y:32321,varname:node_2787,prsc:2|A-7456-OUT,B-4118-OUT;n:type:ShaderForge.SFN_Color,id:1571,x:32933,y:32642,ptovrint:False,ptlb:dark color,ptin:_darkcolor,varname:_darkcolor,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07352942,c2:0.07352942,c3:0.07352942,c4:1;n:type:ShaderForge.SFN_Color,id:6432,x:32933,y:32831,ptovrint:False,ptlb:light color,ptin:_lightcolor,varname:_lightcolor,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:9635,x:33414,y:32606,varname:node_9635,prsc:2|A-1571-RGB,B-6432-RGB,T-4319-RGB;n:type:ShaderForge.SFN_Tex2d,id:4319,x:32858,y:33055,ptovrint:False,ptlb:ramp,ptin:_ramp,varname:_ramp,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:207cd7d6bdf48884581528e5cefd1cd7,ntxv:0,isnm:False|UVIN-5878-OUT;n:type:ShaderForge.SFN_Append,id:5878,x:32449,y:32641,varname:rampUv,prsc:0|A-7397-OUT,B-7397-OUT;n:type:ShaderForge.SFN_Multiply,id:3018,x:34255,y:32537,varname:node_3018,prsc:2|A-9635-OUT,B-1284-OUT;n:type:ShaderForge.SFN_ViewVector,id:3686,x:32935,y:32042,varname:node_3686,prsc:2;n:type:ShaderForge.SFN_Dot,id:1529,x:33161,y:32200,varname:node_1529,prsc:2,dt:0|A-3686-OUT,B-1741-OUT;n:type:ShaderForge.SFN_Subtract,id:7487,x:33532,y:32365,varname:node_7487,prsc:2|A-1017-OUT,B-1529-OUT;n:type:ShaderForge.SFN_Vector1,id:1017,x:33373,y:32307,varname:node_1017,prsc:2,v1:1;n:type:ShaderForge.SFN_Smoothstep,id:2646,x:33820,y:32317,varname:node_2646,prsc:2|A-9270-OUT,B-8504-OUT,V-7487-OUT;n:type:ShaderForge.SFN_Color,id:7625,x:33808,y:32094,ptovrint:False,ptlb:rim color,ptin:_rimcolor,varname:_rimcolor,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8329615,c2:0.8014706,c3:1,c4:0.203;n:type:ShaderForge.SFN_Multiply,id:5370,x:34063,y:32104,varname:node_5370,prsc:2|A-7625-RGB,B-2646-OUT;n:type:ShaderForge.SFN_Multiply,id:7741,x:34255,y:32305,varname:node_7741,prsc:2|A-5370-OUT,B-7625-A;n:type:ShaderForge.SFN_Slider,id:9270,x:33379,y:31956,ptovrint:False,ptlb:min rim value,ptin:_minrimvalue,varname:_minrimvalue,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Slider,id:8504,x:33379,y:32116,ptovrint:False,ptlb:max rim value,ptin:_maxrimvalue,varname:_maxrimvalue,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:2571,x:32633,y:32443,varname:node_2571,prsc:0,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7456,x:32807,y:32321,varname:node_7456,prsc:2|A-4493-OUT,B-2571-OUT;n:type:ShaderForge.SFN_Vector1,id:7851,x:32908,y:32481,varname:node_7851,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:7366,x:33357,y:33029,ptovrint:False,ptlb:dirt texture,ptin:_dirttexture,varname:_dirttexture,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b8ff038b578aab34fad22807bd2c2458,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:1284,x:34063,y:32839,varname:node_1284,prsc:2|A-4934-RGB,B-7366-RGB,T-6004-OUT;n:type:ShaderForge.SFN_Slider,id:4563,x:32938,y:33290,ptovrint:False,ptlb:Dirt,ptin:_Dirt,varname:_Dirt,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:2665,x:32860,y:33435,ptovrint:False,ptlb:DirtMask,ptin:_DirtMask,varname:_DirtMask,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ef9b9b81b8c75d94193c91cdf559c441,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:2011,x:33671,y:33106,varname:node_2011,prsc:0,v1:0;n:type:ShaderForge.SFN_If,id:6004,x:33876,y:33053,varname:node_6004,prsc:2|A-4563-OUT,B-2011-OUT,GT-7536-OUT,EQ-3347-OUT,LT-88-OUT;n:type:ShaderForge.SFN_Posterize,id:2182,x:33095,y:33439,varname:node_2182,prsc:2|IN-2665-RGB,STPS-4795-OUT;n:type:ShaderForge.SFN_Vector1,id:4795,x:32882,y:33651,varname:dirtSteps,prsc:0,v1:5;n:type:ShaderForge.SFN_Step,id:4280,x:33435,y:33314,varname:node_4280,prsc:2|A-4757-OUT,B-4563-OUT;n:type:ShaderForge.SFN_Clamp01,id:4757,x:33266,y:33439,varname:node_4757,prsc:2|IN-2182-OUT;n:type:ShaderForge.SFN_Multiply,id:7536,x:33612,y:33183,varname:node_7536,prsc:2|A-7366-A,B-4280-OUT;n:type:ShaderForge.SFN_Vector1,id:4118,x:32714,y:32501,varname:node_4118,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:3347,x:33682,y:33314,varname:node_3347,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:88,x:33682,y:33382,varname:node_88,prsc:2,v1:0;proporder:4934-1571-6432-4319-7625-9270-8504-7366-4563-2665;pass:END;sub:END;*/

Shader "PinpinAR/Toon with Dirt" {
    Properties {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _darkcolor ("dark color", Color) = (0.07352942,0.07352942,0.07352942,1)
        _lightcolor ("light color", Color) = (1,1,1,1)
        _ramp ("ramp", 2D) = "white" {}
        _rimcolor ("rim color", Color) = (0.8329615,0.8014706,1,0.203)
        _minrimvalue ("min rim value", Range(0, 1)) = 0.5
        _maxrimvalue ("max rim value", Range(0, 1)) = 1
        _dirttexture ("dirt texture", 2D) = "white" {}
        _Dirt ("Dirt", Range(0, 1)) = 1
        _DirtMask ("DirtMask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform fixed4 _darkcolor;
            uniform fixed4 _lightcolor;
            uniform sampler2D _ramp; uniform float4 _ramp_ST;
            uniform fixed4 _rimcolor;
            uniform fixed _minrimvalue;
            uniform fixed _maxrimvalue;
            uniform sampler2D _dirttexture; uniform float4 _dirttexture_ST;
            uniform fixed _Dirt;
            uniform sampler2D _DirtMask; uniform float4 _DirtMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                fixed rampMax = max(((dot(i.normalDir,lightDirection)*0.5)+0.5),0.0);
                fixed2 rampUv = float2(rampMax,rampMax);
                fixed4 _ramp_var = tex2D(_ramp,TRANSFORM_TEX(rampUv, _ramp));
                fixed4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                fixed4 _dirttexture_var = tex2D(_dirttexture,TRANSFORM_TEX(i.uv0, _dirttexture));
                float node_6004_if_leA = step(_Dirt,0.0);
                float node_6004_if_leB = step(0.0,_Dirt);
                fixed4 _DirtMask_var = tex2D(_DirtMask,TRANSFORM_TEX(i.uv0, _DirtMask));
                fixed dirtSteps = 5.0;
                float3 emissive = (lerp(_darkcolor.rgb,_lightcolor.rgb,_ramp_var.rgb)*lerp(_MainTexture_var.rgb,_dirttexture_var.rgb,lerp((node_6004_if_leA*0.0)+(node_6004_if_leB*(_dirttexture_var.a*step(saturate(floor(_DirtMask_var.rgb * dirtSteps) / (dirtSteps - 1)),_Dirt))),0.0,node_6004_if_leA*node_6004_if_leB)));
                float3 finalColor = emissive + ((_rimcolor.rgb*smoothstep( _minrimvalue, _maxrimvalue, (1.0-dot(viewDirection,i.normalDir)) ))*_rimcolor.a);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
