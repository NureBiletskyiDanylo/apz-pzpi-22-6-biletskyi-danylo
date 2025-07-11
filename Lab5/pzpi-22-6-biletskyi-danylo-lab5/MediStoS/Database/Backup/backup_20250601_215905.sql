--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

ALTER TABLE ONLY public.storage_violations DROP CONSTRAINT fk_storage_violations_warehouses_warehouse_id;
ALTER TABLE ONLY public.sensors DROP CONSTRAINT fk_sensors_warehouses_warehouse_id;
ALTER TABLE ONLY public.batches DROP CONSTRAINT fk_batches_warehouses_warehouse_id;
ALTER TABLE ONLY public.batches DROP CONSTRAINT fk_batches_users_user_id;
ALTER TABLE ONLY public.batches DROP CONSTRAINT fk_batches_medicines_medicine_id;
DROP INDEX public."IX_storage_violations_warehouse_id";
DROP INDEX public."IX_sensors_warehouse_id";
DROP INDEX public."IX_batches_warehouse_id";
DROP INDEX public."IX_batches_user_id";
DROP INDEX public."IX_batches_medicine_id";
ALTER TABLE ONLY public.warehouses DROP CONSTRAINT pk_warehouses;
ALTER TABLE ONLY public.users DROP CONSTRAINT pk_users;
ALTER TABLE ONLY public.storage_violations DROP CONSTRAINT pk_storage_violations;
ALTER TABLE ONLY public.sensors DROP CONSTRAINT pk_sensors;
ALTER TABLE ONLY public.medicines DROP CONSTRAINT pk_medicines;
ALTER TABLE ONLY public.batches DROP CONSTRAINT pk_batches;
ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
DROP TABLE public.warehouses;
DROP TABLE public.users;
DROP TABLE public.storage_violations;
DROP TABLE public.sensors;
DROP TABLE public.medicines;
DROP TABLE public.batches;
DROP TABLE public."__EFMigrationsHistory";
SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Name: batches; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.batches (
    id integer NOT NULL,
    batch_number text NOT NULL,
    quantity integer NOT NULL,
    manufacture_date timestamp without time zone NOT NULL,
    expiration_date timestamp without time zone NOT NULL,
    warehouse_id integer NOT NULL,
    user_id integer NOT NULL,
    medicine_id integer NOT NULL
);


ALTER TABLE public.batches OWNER TO postgres;

--
-- Name: batches_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.batches ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.batches_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: medicines; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.medicines (
    id integer NOT NULL,
    name text NOT NULL,
    manufacturer text NOT NULL,
    description text,
    max_temperature real NOT NULL,
    min_temperature real NOT NULL,
    max_humidity real NOT NULL,
    min_humidity real NOT NULL
);


ALTER TABLE public.medicines OWNER TO postgres;

--
-- Name: medicines_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.medicines ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.medicines_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: sensors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sensors (
    id integer NOT NULL,
    type integer NOT NULL,
    serial_number text NOT NULL,
    value real NOT NULL,
    warehouse_id integer NOT NULL
);


ALTER TABLE public.sensors OWNER TO postgres;

--
-- Name: sensors_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.sensors ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.sensors_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: storage_violations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.storage_violations (
    id integer NOT NULL,
    temperature real NOT NULL,
    humidity real NOT NULL,
    recorded_at timestamp without time zone NOT NULL,
    warehouse_id integer NOT NULL
);


ALTER TABLE public.storage_violations OWNER TO postgres;

--
-- Name: storage_violations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.storage_violations ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.storage_violations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    first_name text NOT NULL,
    last_name text NOT NULL,
    email text NOT NULL,
    password_hash bytea NOT NULL,
    password_salt bytea NOT NULL,
    role integer NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.users ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: warehouses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.warehouses (
    id integer NOT NULL,
    name text NOT NULL,
    address text NOT NULL,
    max_temperature real NOT NULL,
    min_temperature real NOT NULL,
    max_humidity real NOT NULL,
    min_humidity real NOT NULL,
    created_at timestamp without time zone NOT NULL
);


ALTER TABLE public.warehouses OWNER TO postgres;

--
-- Name: warehouses_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.warehouses ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.warehouses_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250511143025_InitialCreate	9.0.0
\.


--
-- Data for Name: batches; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.batches (id, batch_number, quantity, manufacture_date, expiration_date, warehouse_id, user_id, medicine_id) FROM stdin;
2	BATCH-16294	723	2023-12-11 15:21:50.546398	2025-08-11 15:21:50.546398	10	1	8
3	BATCH-58781	954	2024-01-11 15:21:50.546875	2025-03-11 15:21:50.546875	1	1	5
4	BATCH-16918	868	2023-12-11 15:21:50.546878	2025-06-11 15:21:50.546878	11	1	5
5	BATCH-44868	695	2025-01-11 15:21:50.546879	2027-07-11 15:21:50.546879	11	1	7
7	BATCH-46272	428	2023-09-11 15:21:50.54688	2025-07-11 15:21:50.54688	11	1	9
9	BATCH-27391	668	2023-11-11 15:21:50.54688	2026-03-11 15:21:50.54688	1	1	5
10	BATCH-92278	428	2024-05-11 15:21:50.54688	2026-01-11 15:21:50.54688	1	1	2
11	BATCH-72957	949	2024-03-11 15:21:50.546881	2025-09-11 15:21:50.546881	8	1	6
13	BATCH-46820	920	2025-04-11 15:21:50.546881	2027-03-11 15:21:50.546881	10	1	6
14	BATCH-36582	145	2025-02-11 15:21:50.546881	2027-06-11 15:21:50.546881	10	1	8
15	BATCH-81997	657	2023-06-11 15:21:50.546881	2024-06-11 15:21:50.546881	3	1	2
16	BATCH-46985	491	2024-04-11 15:21:50.546882	2024-11-11 15:21:50.546882	6	1	6
20	123	1123	2025-05-06 00:00:00	2025-05-07 00:00:00	19	9	4
21	123s	112	2025-05-06 00:00:00	2025-05-31 00:00:00	19	9	9
22	123ffa	112	2025-05-11 00:00:00	2025-05-25 00:00:00	17	9	9
23	SS-2231	120	2025-05-14 00:00:00	2025-06-01 00:00:00	4	9	15
24	SS-331	12	2025-04-30 00:00:00	2025-05-31 00:00:00	4	9	7
8	BATCH-68861	333	2025-03-11 00:00:00	2027-11-11 00:00:00	4	1	7
25	SSaS	1232	2025-05-13 00:00:00	2025-05-29 00:00:00	2	9	10
26	KG-999	20	2025-05-05 00:00:00	2025-05-31 00:00:00	1	9	9
27	KKS-555	133	2025-05-01 00:00:00	2025-06-01 00:00:00	10	9	9
\.


--
-- Data for Name: medicines; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.medicines (id, name, manufacturer, description, max_temperature, min_temperature, max_humidity, min_humidity) FROM stdin;
2	ImmunoPlus0	BioNova	Sample description 0	11	-8	41	37
3	AquaDrop1	PharmaX	Sample description 1	16	-6	62	39
5	OmniCure3	PharmaX	Sample description 3	29	-5	50	23
6	NovaHeal4	CureAll	Sample description 4	11	-2	66	16
7	NovaHeal5	WellnessLabs	Sample description 5	16	-10	44	14
8	NovaHeal6	MediGen	Sample description 6	16	-4	54	19
10	MedAlpha8	HealthCorp	Sample description 8	6	-6	65	26
9	XenMed73	BioNova - ss2	Sample description 7s	16	-37	41	20
15	KK-220-Against headache	Dr220	Medicine against headache	30	15	80	40
4	BetaCure2	HealthCorp	Sample description 2	16	4	48	36
\.


--
-- Data for Name: sensors; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sensors (id, type, serial_number, value, warehouse_id) FROM stdin;
2	0	S-4529	-3.83	4
3	1	S-6781	49.81	6
5	1	S-1617	70.34	3
7	0	S-4177	20.31	5
8	0	S-1403	1	11
9	1	S-5043	60.39	9
10	1	S-8177	80.36	3
12	0	S-5139	-8.81	6
13	1	S-3791	42.03	11
14	0	S-5735	28.53	11
15	1	S-9395	63.48	4
16	1	S-5064	78.57	10
17	1	S-1764	72.41	2
18	1	S-3083	51.5	3
19	0	S-1174	10.95	6
20	1	S-2104	52.26	6
21	0	S-2068	27.74	2
27	0	SS1	0	2
29	1	SSF-223	0	21
28	1	KKF-55	98.106865	1
6	1	S-6669	100	1
22	1	s231	100	1
25	0	S-123a	-0.234442	1
1	0	s-1	36.279564	1
4	0	S-3827	-4.790076	1
\.


--
-- Data for Name: storage_violations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.storage_violations (id, temperature, humidity, recorded_at, warehouse_id) FROM stdin;
1	12	34.89	2025-05-12 13:55:41.492169	1
2	-14.376867	34.89	2025-05-28 12:48:02.813089	1
3	36.30404	34.89	2025-05-28 12:48:02.986739	1
4	7.309057	100	2025-05-28 13:19:31.825134	1
5	7.309057	100	2025-05-28 13:19:31.97852	1
6	5.862787	100	2025-05-28 13:19:32.002212	1
7	19.066065	100	2025-05-28 13:19:32.021111	1
8	-2.932654	100	2025-05-28 13:19:32.038251	1
9	7.332066	100	2025-05-28 13:19:33.16206	1
10	7.332066	100	2025-05-28 13:19:33.300705	1
11	27.359919	100	2025-05-28 13:19:33.445001	1
12	-18.391592	100	2025-05-28 13:19:33.461865	1
13	35.808098	100	2025-05-28 13:19:33.581587	1
14	14.925475	74.635666	2025-05-28 13:19:34.608792	1
15	14.925475	100	2025-05-28 13:19:34.627513	1
16	47.039696	87.31783	2025-05-28 13:19:34.668374	1
17	43.948296	87.31783	2025-05-28 13:19:34.716563	1
18	-4.156256	87.31783	2025-05-28 13:19:34.792753	1
19	28.943913	58.103966	2025-05-28 13:22:17.55067	1
20	28.943913	86.2356	2025-05-28 13:22:17.635004	1
21	48.547752	72.169785	2025-05-28 13:22:17.687422	1
22	-16.874487	72.169785	2025-05-28 13:22:17.703609	1
23	36.98916	72.169785	2025-05-28 13:22:17.720508	1
24	22.887474	81.155685	2025-05-28 13:22:18.753645	1
25	22.887474	100	2025-05-28 13:22:18.770973	1
26	-2.885504	90.57784	2025-05-28 13:22:18.787957	1
27	2.916925	90.57784	2025-05-28 13:22:18.804516	1
28	37.562645	90.57784	2025-05-28 13:22:18.820291	1
29	12.531355	100	2025-05-28 13:22:19.94796	1
30	12.531355	73.95642	2025-05-28 13:22:20.023459	1
31	20.361963	86.97821	2025-05-28 13:22:20.089924	1
32	26.784388	86.97821	2025-05-28 13:22:20.106468	1
33	-17.087744	86.97821	2025-05-28 13:22:20.123057	1
34	10.019536	75.98876	2025-05-28 13:22:21.146513	1
35	10.019536	100	2025-05-28 13:22:25.353561	1
36	-5.608832	87.994385	2025-05-28 13:22:25.36687	1
37	29.668024	87.994385	2025-05-28 13:22:25.380099	1
38	0.708429	87.994385	2025-05-28 13:22:25.393203	1
39	8.255874	100	2025-05-28 13:22:26.421609	1
40	8.255874	100	2025-05-28 13:22:26.434443	1
41	48.422295	100	2025-05-28 13:22:26.447655	1
42	-6.14411	100	2025-05-28 13:22:26.460769	1
43	-0.020483	100	2025-05-28 13:22:26.473076	1
44	14.0859	95.878914	2025-05-28 13:22:27.498749	1
45	14.0859	100	2025-05-28 13:22:27.513434	1
46	6.912164	97.93945	2025-05-28 13:22:27.526769	1
47	44.727196	97.93945	2025-05-28 13:22:27.541852	1
48	47.75344	97.93945	2025-05-28 13:22:27.554981	1
49	33.13093	56.13781	2025-05-28 13:22:28.586366	1
50	33.13093	28.740448	2025-05-28 13:22:28.601691	1
51	10.14477	42.43913	2025-05-28 13:22:28.614751	1
52	33.281395	42.43913	2025-05-28 13:22:28.627469	1
53	2.348382	42.43913	2025-05-28 13:22:28.640634	1
54	15.258183	100	2025-05-28 13:22:29.657126	1
55	15.258183	60.011368	2025-05-28 13:22:29.669913	1
56	24.311945	80.005684	2025-05-28 13:22:29.682512	1
57	-0.830389	80.005684	2025-05-28 13:22:29.695609	1
58	45.498924	80.005684	2025-05-28 13:22:29.72829	1
59	22.993494	100	2025-05-28 13:22:30.761314	1
60	22.993494	26.107746	2025-05-28 13:22:30.777126	1
61	41.891747	63.05387	2025-05-28 13:22:30.796973	1
62	36.654686	63.05387	2025-05-28 13:22:30.812061	1
63	2.2268	63.05387	2025-05-28 13:22:30.826055	1
64	26.924412	57.92297	2025-05-28 13:22:31.847674	1
65	26.924412	65.07424	2025-05-28 13:22:31.86119	1
66	47.918755	61.498604	2025-05-28 13:22:31.873901	1
67	-5.983435	61.498604	2025-05-28 13:22:31.885557	1
68	14.449121	61.498604	2025-05-28 13:22:31.898076	1
69	18.794813	100	2025-05-28 13:22:32.930379	1
70	18.794813	41.87661	2025-05-28 13:22:32.943789	1
71	16.050592	70.93831	2025-05-28 13:22:32.955842	1
72	47.2073	70.93831	2025-05-28 13:22:32.970307	1
73	11.891657	70.93831	2025-05-28 13:22:32.983082	1
74	25.049849	71.67983	2025-05-28 13:22:34.012739	1
75	25.049849	95.16971	2025-05-28 13:22:34.025582	1
76	9.803217	83.424774	2025-05-28 13:22:34.03867	1
77	28.95901	83.424774	2025-05-28 13:22:34.05486	1
78	34.523064	83.424774	2025-05-28 13:22:34.067242	1
79	24.42843	100	2025-05-28 13:24:22.460787	1
80	24.42843	100	2025-05-28 13:24:22.476139	1
81	-16.80161	100	2025-05-28 13:24:22.491812	1
82	-10.256231	100	2025-05-28 13:24:22.505128	1
83	12.250727	100	2025-05-28 13:24:22.535717	1
84	-4.935705	100	2025-05-28 13:24:32.563573	1
85	-4.935705	100	2025-05-28 13:24:32.57829	1
86	19.806238	100	2025-05-28 13:24:32.590576	1
87	29.026789	100	2025-05-28 13:24:32.602903	1
88	47.604393	100	2025-05-28 13:24:32.613996	1
89	32.145805	68.43188	2025-05-28 13:24:42.629662	1
90	32.145805	70.659195	2025-05-28 13:24:42.642534	1
91	27.84109	69.54553	2025-05-28 13:24:42.654084	1
92	30.03213	69.54553	2025-05-28 13:24:42.666686	1
93	13.733027	69.54553	2025-05-28 13:24:42.678138	1
94	23.868748	100	2025-05-28 13:24:52.705555	1
95	23.868748	100	2025-05-28 13:24:52.71801	1
96	-16.158148	100	2025-05-28 13:24:52.729946	1
97	6.233758	100	2025-05-28 13:24:52.742076	1
98	36.51992	100	2025-05-28 13:24:52.75411	1
99	8.865177	100	2025-05-28 13:33:28.256101	1
100	8.865177	100	2025-05-28 13:33:28.272033	1
101	3.9591	100	2025-05-28 13:33:28.285684	1
102	12.326044	100	2025-05-28 13:33:28.299266	1
103	29.633478	100	2025-05-28 13:33:28.31208	1
104	15.306208	95.01588	2025-05-28 13:33:38.345516	1
105	15.306208	100	2025-05-28 13:33:38.360698	1
106	7.698602	97.507935	2025-05-28 13:33:38.374615	1
107	-11.816961	97.507935	2025-05-28 13:33:38.387775	1
108	10.391718	97.507935	2025-05-28 13:33:38.401475	1
109	2.0911195	100	2025-05-28 13:33:48.422471	1
110	2.0911195	100	2025-05-28 13:33:48.435588	1
111	-4.777625	100	2025-05-28 13:33:48.448158	1
112	11.440149	100	2025-05-28 13:33:48.461215	1
113	5.002038	100	2025-05-28 13:33:48.474532	1
114	3.8881874	100	2025-05-28 13:33:58.501349	1
115	3.8881874	100	2025-05-28 13:33:58.515396	1
116	9.745081	100	2025-05-28 13:33:58.528302	1
117	9.043573	100	2025-05-28 13:33:58.541026	1
118	3.594324	100	2025-05-28 13:33:58.553893	1
119	7.460993	100	2025-05-28 13:34:08.576099	1
120	7.460993	100	2025-05-28 13:34:08.588184	1
121	5.556085	100	2025-05-28 13:34:08.600246	1
122	-10.51383	100	2025-05-28 13:34:08.612538	1
123	10.588129	100	2025-05-28 13:34:08.628091	1
124	1.8767947	100	2025-05-28 13:34:18.661274	1
125	1.8767947	100	2025-05-28 13:34:18.675539	1
126	36.587303	100	2025-05-28 13:34:18.691322	1
127	-2.26142	100	2025-05-28 13:34:18.703496	1
128	45.25988	100	2025-05-28 13:34:18.715256	1
129	26.528587	82.77359	2025-05-28 13:34:28.729866	1
130	26.528587	64.79498	2025-05-28 13:34:28.745957	1
131	38.525444	73.78429	2025-05-28 13:34:28.774672	1
132	38.37188	73.78429	2025-05-28 13:34:28.78732	1
133	42.386497	73.78429	2025-05-28 13:34:28.798855	1
134	39.761272	32.15551	2025-05-28 13:34:38.825865	1
135	39.761272	27.036636	2025-05-28 13:34:38.83866	1
136	7.351434	29.596073	2025-05-28 13:34:38.851406	1
137	7.22384	29.596073	2025-05-28 13:34:38.86293	1
138	-8.468847	29.596073	2025-05-28 13:34:38.876016	1
139	2.0354757	100	2025-05-28 13:34:48.894639	1
140	2.0354757	100	2025-05-28 13:34:48.909036	1
141	-10.71488	100	2025-05-28 13:34:48.926261	1
142	-2.278327	100	2025-05-28 13:34:48.941962	1
143	39.38781	100	2025-05-28 13:34:48.956517	1
144	8.798201	100	2025-05-28 13:34:59.005803	1
145	8.798201	100	2025-05-28 13:34:59.019393	1
146	-11.352453	100	2025-05-28 13:34:59.032375	1
147	2.077508	100	2025-05-28 13:34:59.046496	1
148	-3.155341	100	2025-05-28 13:34:59.060008	1
149	-4.143429	100	2025-05-30 17:42:31.327994	1
150	-4.143429	100	2025-05-30 17:42:31.363397	1
151	-2.858954	100	2025-05-30 17:42:31.405647	1
152	37.87731	100	2025-05-30 17:42:31.42292	1
153	21.031437	100	2025-05-30 17:42:41.438181	1
154	21.031437	100	2025-05-30 17:42:41.452926	1
155	48.18493	100	2025-05-30 17:42:41.466063	1
156	-9.072058	100	2025-05-30 17:42:41.522525	1
157	37.750175	100	2025-05-30 17:42:41.535761	1
158	25.621016	52.16176	2025-05-30 17:42:51.551752	1
159	25.621016	100	2025-05-30 17:42:51.566537	1
160	34.66134	76.08088	2025-05-30 17:42:51.598833	1
161	22.569654	66.009315	2025-05-30 17:43:01.641094	1
162	22.569654	84.40188	2025-05-30 17:43:01.657917	1
163	34.35015	75.2056	2025-05-30 17:43:01.739381	1
164	30.654188	73.06351	2025-05-30 17:43:11.767545	1
165	14.664576	60.87316	2025-05-30 17:43:11.782941	1
166	-15.433966	60.87316	2025-05-30 17:43:11.795552	1
167	-14.977972	60.87316	2025-05-30 17:43:11.849861	1
168	-3.9368405	100	2025-05-30 17:43:21.864751	1
169	-3.9368405	100	2025-05-30 17:43:21.882396	1
170	14.79162	100	2025-05-30 17:43:21.896847	1
171	35.988976	100	2025-05-30 17:43:21.91396	1
172	0.885248	100	2025-05-30 17:43:21.930081	1
173	12.916461	100	2025-05-30 17:43:31.966326	1
174	12.916461	100	2025-05-30 17:43:31.978766	1
175	40.936485	100	2025-05-30 17:43:31.991624	1
176	47.994408	100	2025-05-30 17:43:32.003638	1
177	3.321741	100	2025-05-30 17:43:32.016166	1
178	27.508049	82.407196	2025-05-30 17:44:23.149927	1
179	0.472375	60.64597	2025-05-30 17:44:23.166964	1
180	45.708855	60.64597	2025-05-30 17:44:23.183734	1
181	7.999689	60.64597	2025-05-30 17:44:23.201325	1
182	-2.742957	60.64597	2025-05-30 17:44:33.217044	1
183	12.85949	100	2025-05-30 17:44:33.245046	1
184	-18.76383	72.35069	2025-05-30 17:44:33.258513	1
185	33.936302	72.35069	2025-05-30 17:44:33.301946	1
186	12.728548	100	2025-05-30 17:44:43.33183	1
187	12.728548	100	2025-05-30 17:44:43.345722	1
188	35.77056	100	2025-05-30 17:44:43.35721	1
189	-11.502691	100	2025-05-30 17:44:43.370293	1
190	34.243126	100	2025-05-30 17:44:43.384653	1
191	-2.968817	100	2025-05-30 17:45:34.570579	1
192	45.179344	100	2025-05-30 17:45:34.687676	1
193	6.825281	100	2025-05-30 17:45:34.738816	1
194	16.34527	100	2025-06-01 19:57:36.861288	1
195	16.34527	100	2025-06-01 19:57:36.89059	1
196	16.34527	100	2025-06-01 19:57:36.910125	1
197	2.400995	100	2025-06-01 19:57:36.942162	1
198	-14.368048	100	2025-06-01 19:57:36.957171	1
199	5.105107	95.26883	2025-06-01 19:57:46.974211	1
200	5.105107	90.97923	2025-06-01 19:57:46.989664	1
201	5.105107	100	2025-06-01 19:57:47.003368	1
202	3.226709	95.41602	2025-06-01 19:57:47.016537	1
203	39.168144	95.41602	2025-06-01 19:57:47.030555	1
204	-13.942785	95.41602	2025-06-01 19:57:47.044508	1
205	9.484022	100	2025-06-01 19:57:57.094255	1
206	9.484022	100	2025-06-01 19:57:57.107725	1
207	9.484022	52.707474	2025-06-01 19:57:57.142357	1
208	-6.100165	84.235825	2025-06-01 19:57:57.155657	1
209	36.878593	84.235825	2025-06-01 19:57:57.168483	1
210	-13.630621	84.235825	2025-06-01 19:57:57.18322	1
211	5.7159357	98.106865	2025-06-01 19:58:07.213829	1
212	5.7159357	100	2025-06-01 19:58:07.227626	1
213	5.7159357	100	2025-06-01 19:58:07.241132	1
214	-0.234442	99.36896	2025-06-01 19:58:07.25494	1
215	36.279564	99.36896	2025-06-01 19:58:07.269264	1
216	-4.790076	99.36896	2025-06-01 19:58:07.284385	1
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, first_name, last_name, email, password_hash, password_salt, role) FROM stdin;
1	kira	liko	kirashka@gmail.com	\\xa65dd80e4093cc30ca73abb3752190cef761c5ee8b7b688b45cb46388da9f831f5e4a7647491839098ae98c65de1d5c61759e826df04e5d90528759534a758fd	\\x87d5590c776b5fcf6b9abc1d02de8c0695c12d43e42e0564990b8ff923ba10c8c5c79d8aa930b888fd6caa99f2eadaa70611725b66acc6e950ec7c8f9fce24f92966fe2ea00ae5919ce7e676022c924fe765e78f449855768fa65da915e1a6a9bb89ed8ddbda5865719d1418393b28632e74f1067cfaf799f317b2cfae0963ab	2
2	lalo	lalo	lalo@gmail.com	\\x37b690a2ea82b20630287550e12aaa06f77ac71adfdf064b1f7c47f932438e948a43545f7220c777b5c9992a15dd81665980ba692c78fe6354c906c5360b6e6e	\\x856dfa5f28fbcf7cb24e787e58ed9699476ce91e1c63e6fa62a17565aeeb799b546c85e2012ed590f6e39e9b0789b88ca94ade4bedac2bedf1ae70b0bb53b60a0316a2777be6386ae2132bc8b467ade95de3b73913e0bc6222639e9b36a8cb912fb34c752e5a83e73c5ecf5b6bacff7b4b266eae89ce5a10b657b5193d63ee2c	3
5	lalo	kirka	lalo3@gmail.com	\\x159127689218f2eadc76b636b540aaf9f25721064eb9faeb9a282377b51a5f1224ef44fe04daec14b8bfa971855f61c622d4077cee8bf06252b123f67118976a	\\x2f5d7a0f77e3c7d12e8ea99bb09c73190538098af5e8b0144e7f2fd664ec87e51cb9d3070261d237bbdf9c5dc7e37cd99b2b1cc6d43ff25e6aea0bd617997c46050546bd5f995657e88869782bf5b0b0b3a2b1dc6c4aa7fd33077e86a6e821778c2e8f45ff5302406d0572c28febd977840dc18b8a853072ee2cdc582a5be391	0
9	ksao	kaso	sa@gmail.com	\\x439056cb5227ad0d4b1fcd3608ec92898e10aab25187c6db217e306193326007e401083f29ca06779695571a97c08fc43be1317958d6ee626ce1ba823ce649c0	\\x04682a290e7edb38bb24ea75cd06a1b61b2e7f9068b3c1aef34e63426d6500f0f9cd75fb9e0ddc74e5dd9c0c138e080e747d3dfb31ea3bf7dfc75241f4a6eaa71ef4d67d5d6139fd103265d981b177f02d92224f2563f844697469d3d5a121c498fb12ed626f68385695c3a5e2f536c4a23bd308db857034dc5e0dd75e5b1f69	2
4	Lakia	maroon	lalo2@gmail.com	\\x8f66b132f80cafaa779b96e55d4c8a10264ed9b5ed17ee59c0a113a89c1bfd62e045d509f50b83c9f26e2732b1d7b7f4aaf36ab579939e078ed32989dd9164f7	\\x6fcdccefeaacffa42d7f31da641c12eff3a4345dec10e51112316a6a377b0c9fb89b9aca5e60568c1017794fc00d26aa509196ee0afc9521d6861043e701ce1547be035228cc9243bd5b0660ac9bee85de6a547a0cbc99fe643784af3e8977895ee319241185bd5ef16f204be34d010058f086e8456fa9a62453ae734921e68e	3
10	Danylo	Marcu	someemail@gmail.com	\\xbbbe52fcca9e88b7af346623cb8b31e7fadb5092e214538f6b5a12ad2c2267ee188f85481dacaeba8bfe3738e238a06756707fea90de979696889ba918567449	\\xc97e010a8091c05742c10058f28331207a81a7c1e0e5a12619bf83a4ea635ff4a97030e4bfa36972907b9d55c8e9f3a2d5496138387e50e55bfa8a5e1c9f9a954517ebde1f5c087fc75b398ba45b9b580b15c0cb32dff8f69d1b8dd99379a292b88331074ed8bb07c6eb4864ea62adf666abaa28974b52963cba76e4180b2dd6	0
12	Leonardo	LeoRidio	kikaro@gmail.com	\\xa010900a24bb6c33718e0964a00ab871cae1bb62e71d6c9884df3721179dec45f57d10209bd46953d10a36d8839894c86216b28965267d3cff464e051a0ad8ba	\\xeda7b7e186df5cb24064e2b2a52f53daed0a6322481b962f11395c7c74b990dc47365481bb9185314f72f900602d49f693002110e238cc62eef780d91a07d30d872bea457e4d3149d6fe391f4b72afcb470ed78d2362bd3913462d0c6104e24c1993155a9555aadb854ce8b4f966c91025d021acff976a58f7d7debf1c9d644d	3
\.


--
-- Data for Name: warehouses; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.warehouses (id, name, address, max_temperature, min_temperature, max_humidity, min_humidity, created_at) FROM stdin;
2	Central Unit A	789 Gamma Rd.	6	-5	45	31	2024-10-12 15:07:10.389378
3	South Storage B	505 Iota Way	21	-18	74	39	2024-06-22 15:07:10.389403
4	East Facility C	123 Alpha St.	1	-10	62	28	2024-10-03 15:07:10.389404
5	West Hub D	123 Alpha St.	13	-8	44	21	2025-04-06 15:07:10.389404
6	Prime Storage E	123 Alpha St.	22	-14	81	19	2024-05-18 15:07:10.389404
7	Sigma Depot F	101 Delta Blvd.	16	-4	68	17	2024-09-02 15:07:10.389405
8	South Storage G	303 Zeta Dr.	18	-11	62	32	2024-10-25 15:07:10.389405
9	Delta Warehouse H	101 Delta Blvd.	3	-17	64	32	2024-10-22 15:07:10.389405
10	Prime Storage I	303 Zeta Dr.	19	-6	68	18	2024-09-25 15:07:10.389405
11	Omega Storage J	123 Alpha St.	6	-8	63	28	2024-11-23 15:07:10.389405
17	s	s	0	0	0	0	2025-05-26 20:16:28.515974
18	as	as	32	22	32	22	2025-05-26 20:17:22.959793
19	Warehouse-3421	Wallstreet-23	50	20	50	20	2025-05-28 11:11:34.897282
1	Warehouse-1	Sw-1	30	15	50	20	2025-05-11 14:36:39.61296
21	L-k223	Golden Street 25	40	15	70	30	2025-06-01 19:53:49.823941
\.


--
-- Name: batches_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.batches_id_seq', 29, true);


--
-- Name: medicines_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.medicines_id_seq', 17, true);


--
-- Name: sensors_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sensors_id_seq', 29, true);


--
-- Name: storage_violations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.storage_violations_id_seq', 216, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 12, true);


--
-- Name: warehouses_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.warehouses_id_seq', 21, true);


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: batches pk_batches; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.batches
    ADD CONSTRAINT pk_batches PRIMARY KEY (id);


--
-- Name: medicines pk_medicines; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.medicines
    ADD CONSTRAINT pk_medicines PRIMARY KEY (id);


--
-- Name: sensors pk_sensors; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT pk_sensors PRIMARY KEY (id);


--
-- Name: storage_violations pk_storage_violations; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.storage_violations
    ADD CONSTRAINT pk_storage_violations PRIMARY KEY (id);


--
-- Name: users pk_users; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT pk_users PRIMARY KEY (id);


--
-- Name: warehouses pk_warehouses; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.warehouses
    ADD CONSTRAINT pk_warehouses PRIMARY KEY (id);


--
-- Name: IX_batches_medicine_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_batches_medicine_id" ON public.batches USING btree (medicine_id);


--
-- Name: IX_batches_user_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_batches_user_id" ON public.batches USING btree (user_id);


--
-- Name: IX_batches_warehouse_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_batches_warehouse_id" ON public.batches USING btree (warehouse_id);


--
-- Name: IX_sensors_warehouse_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_sensors_warehouse_id" ON public.sensors USING btree (warehouse_id);


--
-- Name: IX_storage_violations_warehouse_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_storage_violations_warehouse_id" ON public.storage_violations USING btree (warehouse_id);


--
-- Name: batches fk_batches_medicines_medicine_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.batches
    ADD CONSTRAINT fk_batches_medicines_medicine_id FOREIGN KEY (medicine_id) REFERENCES public.medicines(id) ON DELETE CASCADE;


--
-- Name: batches fk_batches_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.batches
    ADD CONSTRAINT fk_batches_users_user_id FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- Name: batches fk_batches_warehouses_warehouse_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.batches
    ADD CONSTRAINT fk_batches_warehouses_warehouse_id FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) ON DELETE CASCADE;


--
-- Name: sensors fk_sensors_warehouses_warehouse_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT fk_sensors_warehouses_warehouse_id FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) ON DELETE CASCADE;


--
-- Name: storage_violations fk_storage_violations_warehouses_warehouse_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.storage_violations
    ADD CONSTRAINT fk_storage_violations_warehouses_warehouse_id FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

