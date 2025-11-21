Shader "Custom/RetroTransition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0,0,0,1) // 기본 검은색
        _Progress ("Progress", Range(0,1)) = 0 // 0이면 투명, 1이면 완전 암전
        _Size ("Pattern Size", Float) = 50 // 다이아몬드/픽셀 크기
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Progress;
            float _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 화면 비율에 맞춰 UV 조정 (정사각형 패턴 유지)
                float2 uv = i.uv;
                uv.x *= _ScreenParams.x / _ScreenParams.y;

                // 다이아몬드 패턴 생성 로직
                float2 pos = floor(uv * _Size) / _Size;
                float pattern = pos.x + pos.y; 
                
                // *TIP: 픽셀 모자이크 느낌을 원하면 아래 로직을 변경 가능
                // 여기서는 영상과 유사한 사선/다이아몬드 느낌의 컷오프 구현
                float logicalXY = i.uv.x + i.uv.y; 
                
                // 진행도(_Progress)에 따라 알파값 결정
                // _Progress가 커질수록 검은색 영역이 늘어남
                float alpha = step(1 - _Progress, frac(logicalXY * _Size)); 
                
                // 혹은 더 단순한 픽셀 컷을 원한다면 아래 한 줄을 사용하세요:
                // float alpha = (_Progress > 0.0) ? 1.0 : 0.0; // (심플 페이드시)

                // 쉐이더 기반의 부드러운 컷오프가 아닌 딱딱한 픽셀 느낌
                if(_Progress >= 1.0) return _Color; // 완전 암전
                if(_Progress <= 0.0) return fixed4(0,0,0,0); // 완전 투명

                return fixed4(_Color.rgb, _Progress); 
            }
            ENDCG
        }
    }
}