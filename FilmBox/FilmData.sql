PGDMP  ;    5                 |            FilmData    16.1    16.1     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16777    FilmData    DATABASE     ~   CREATE DATABASE "FilmData" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Turkish_Turkey.1254';
    DROP DATABASE "FilmData";
                postgres    false            �            1259    16778    FilmBilgisi    TABLE     �   CREATE TABLE public."FilmBilgisi" (
    "FilmAdi" text NOT NULL,
    "Yönetmen" text,
    "Oyuncular" text,
    "FilmTuru" text,
    "DPuan" double precision,
    "YayinYili" integer,
    "IzlenmeSayisi" integer
);
 !   DROP TABLE public."FilmBilgisi";
       public         heap    postgres    false            �            1259    16783    FilmIzlemeListesi    TABLE     t   CREATE TABLE public."FilmIzlemeListesi" (
    "FilmAdi" text,
    "KullaniciAdi" text,
    "Id" integer NOT NULL
);
 '   DROP TABLE public."FilmIzlemeListesi";
       public         heap    postgres    false            �            1259    16788    FilmIzlemeListesi_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."FilmIzlemeListesi_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE public."FilmIzlemeListesi_Id_seq";
       public          postgres    false    216            �           0    0    FilmIzlemeListesi_Id_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public."FilmIzlemeListesi_Id_seq" OWNED BY public."FilmIzlemeListesi"."Id";
          public          postgres    false    217            �            1259    16789    FilmPuanYorum    TABLE     �   CREATE TABLE public."FilmPuanYorum" (
    "DegerlendirmeId" integer NOT NULL,
    "FilmAdi" text,
    "Puan" integer,
    "KullaniciAdi" text,
    "Yorum" text
);
 #   DROP TABLE public."FilmPuanYorum";
       public         heap    postgres    false            �            1259    16794 !   FilmPuanYorum_DegerlendirmeId_seq    SEQUENCE     �   CREATE SEQUENCE public."FilmPuanYorum_DegerlendirmeId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 :   DROP SEQUENCE public."FilmPuanYorum_DegerlendirmeId_seq";
       public          postgres    false    218            �           0    0 !   FilmPuanYorum_DegerlendirmeId_seq    SEQUENCE OWNED BY     m   ALTER SEQUENCE public."FilmPuanYorum_DegerlendirmeId_seq" OWNED BY public."FilmPuanYorum"."DegerlendirmeId";
          public          postgres    false    219            �            1259    16795    Kullanicilar    TABLE     �   CREATE TABLE public."Kullanicilar" (
    "KullaniciAdi" text,
    "Tc" bigint NOT NULL,
    "Ad" text,
    "Soyad" text,
    "DogumTarihi" date,
    "Cinsiyet" "char",
    "UyelikTuru" text,
    "Sifre" text
);
 "   DROP TABLE public."Kullanicilar";
       public         heap    postgres    false            '           2604    16800    FilmIzlemeListesi Id    DEFAULT     �   ALTER TABLE ONLY public."FilmIzlemeListesi" ALTER COLUMN "Id" SET DEFAULT nextval('public."FilmIzlemeListesi_Id_seq"'::regclass);
 G   ALTER TABLE public."FilmIzlemeListesi" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    217    216            (           2604    16801    FilmPuanYorum DegerlendirmeId    DEFAULT     �   ALTER TABLE ONLY public."FilmPuanYorum" ALTER COLUMN "DegerlendirmeId" SET DEFAULT nextval('public."FilmPuanYorum_DegerlendirmeId_seq"'::regclass);
 P   ALTER TABLE public."FilmPuanYorum" ALTER COLUMN "DegerlendirmeId" DROP DEFAULT;
       public          postgres    false    219    218            �          0    16778    FilmBilgisi 
   TABLE DATA              COPY public."FilmBilgisi" ("FilmAdi", "Yönetmen", "Oyuncular", "FilmTuru", "DPuan", "YayinYili", "IzlenmeSayisi") FROM stdin;
    public          postgres    false    215   �       �          0    16783    FilmIzlemeListesi 
   TABLE DATA           N   COPY public."FilmIzlemeListesi" ("FilmAdi", "KullaniciAdi", "Id") FROM stdin;
    public          postgres    false    216   �       �          0    16789    FilmPuanYorum 
   TABLE DATA           h   COPY public."FilmPuanYorum" ("DegerlendirmeId", "FilmAdi", "Puan", "KullaniciAdi", "Yorum") FROM stdin;
    public          postgres    false    218   �       �          0    16795    Kullanicilar 
   TABLE DATA              COPY public."Kullanicilar" ("KullaniciAdi", "Tc", "Ad", "Soyad", "DogumTarihi", "Cinsiyet", "UyelikTuru", "Sifre") FROM stdin;
    public          postgres    false    220   @       �           0    0    FilmIzlemeListesi_Id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."FilmIzlemeListesi_Id_seq"', 7, true);
          public          postgres    false    217            �           0    0 !   FilmPuanYorum_DegerlendirmeId_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public."FilmPuanYorum_DegerlendirmeId_seq"', 5, true);
          public          postgres    false    219            *           2606    16803    FilmBilgisi FilmBilgisi_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."FilmBilgisi"
    ADD CONSTRAINT "FilmBilgisi_pkey" PRIMARY KEY ("FilmAdi");
 J   ALTER TABLE ONLY public."FilmBilgisi" DROP CONSTRAINT "FilmBilgisi_pkey";
       public            postgres    false    215            ,           2606    16805 (   FilmIzlemeListesi FilmIzlemeListesi_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public."FilmIzlemeListesi"
    ADD CONSTRAINT "FilmIzlemeListesi_pkey" PRIMARY KEY ("Id");
 V   ALTER TABLE ONLY public."FilmIzlemeListesi" DROP CONSTRAINT "FilmIzlemeListesi_pkey";
       public            postgres    false    216            .           2606    16807     FilmPuanYorum FilmPuanYorum_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public."FilmPuanYorum"
    ADD CONSTRAINT "FilmPuanYorum_pkey" PRIMARY KEY ("DegerlendirmeId");
 N   ALTER TABLE ONLY public."FilmPuanYorum" DROP CONSTRAINT "FilmPuanYorum_pkey";
       public            postgres    false    218            0           2606    16809    Kullanicilar Kullanicilar_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Kullanicilar"
    ADD CONSTRAINT "Kullanicilar_pkey" PRIMARY KEY ("Tc");
 L   ALTER TABLE ONLY public."Kullanicilar" DROP CONSTRAINT "Kullanicilar_pkey";
       public            postgres    false    220            �   �  x�]��N�@��ӧ�DUZ�-9���*���b��Xd4�������>�j�O�q����~�N�{��l������VzA(��(��0��u�)��kM?l#��ڶY�vю�j�33/gfv^��>b���X�3�=�p��,?�N�
�){�-�r�1�b8zJfi����y0U.+3;��[q̃�-�wZ��N�o�5�� ��o�`I�ފˀ�d�A���3\z��W�+
�c�O��^ef܍7�`c���J�p�1R:�{� ���	y��Qŭm:Bz��|KaV'�!��m���fTU�f>Ϩ�������(��U��
_��B�G�Ig�5$Xd�SR��H��X�����1��p2�#������O�h�Ѷ���y[;i�+yo�ÿ���+�2�.2��t2��䨼      �   C   x�s,,M�M��L�*�/�4��,�KN�V���N�L-΄Jsy�g�Ay&P�kqnbe%�9W� �/�      �   I   x�3����N-�4�t-�M���L?��*5�˔3$�$1/3��&s�=?[���2��81G!)�H!-3'�+F��� ^,�      �   �   x�M��
�0�ϛw�d��ߵғO/[M�����C}0_�s���mg�c����q����(�:Ǒ`UfQ�8�nW�}BP�#�Xr��b�ki#��������T�L�m�$@^�ZH�e�uH�щ�Lª��_���Y銜�����>|:�j�:/��e��zUL?a     