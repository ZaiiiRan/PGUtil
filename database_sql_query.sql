--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1
-- Dumped by pg_dump version 16.1

-- Started on 2024-05-06 01:31:53

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4985 (class 1262 OID 41837)
-- Name: photo_workshop; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE photo_workshop WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE photo_workshop OWNER TO postgres;

\connect photo_workshop

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 240 (class 1255 OID 50188)
-- Name: abort_alter_view_command(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.abort_alter_view_command() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $$
	BEGIN
		RAISE EXCEPTION 'Команда % отключена в БД: %', tg_tag, current_database();
	END;
$$;


ALTER FUNCTION public.abort_alter_view_command() OWNER TO postgres;

--
-- TOC entry 236 (class 1255 OID 41997)
-- Name: before_delete_from_workers(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.before_delete_from_workers() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
	BEGIN
		RAISE NOTICE 'Удаляемый работник с inn = %', OLD.inn;
		RETURN OLD;
	END;
$$;


ALTER FUNCTION public.before_delete_from_workers() OWNER TO postgres;

--
-- TOC entry 235 (class 1255 OID 41995)
-- Name: check_inn(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_inn() RETURNS trigger
    LANGUAGE plpgsql
    AS $_$
	BEGIN
		IF NEW.inn IS NULL THEN 
			RAISE EXCEPTION 'Укажите ИНН!';
		END IF;
		IF NEW.inn  !~ '^\d{12}$' THEN 
			RAISE EXCEPTION 'ИНН должен состоять из 12 цифр!';
		END IF;
		RETURN NEW;
	END;
$_$;


ALTER FUNCTION public.check_inn() OWNER TO postgres;

--
-- TOC entry 239 (class 1255 OID 50186)
-- Name: delete_work(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.delete_work() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
	BEGIN
		IF (OLD.is_deleted IS NULL) or (OLD.is_deleted = false) THEN
			EXECUTE 'UPDATE works SET is_deleted = true WHERE id = '
				|| OLD.id || ';';
			RETURN NULL;
		END IF;
		RETURN OLD;
	END;
$$;


ALTER FUNCTION public.delete_work() OWNER TO postgres;

--
-- TOC entry 237 (class 1255 OID 42011)
-- Name: log_customers(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.log_customers() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
	BEGIN
		INSERT INTO logs (time_stamp, info) VALUES 
			(NOW(), CONCAT('UPDATE customer ', NEW.id));
		RETURN NEW;
	END;
$$;


ALTER FUNCTION public.log_customers() OWNER TO postgres;

--
-- TOC entry 238 (class 1255 OID 42014)
-- Name: log_orders(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.log_orders() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
	BEGIN
		INSERT INTO logs (time_stamp, info) VALUES 
			(NOW(), CONCAT('UPDATE order ', NEW.id));
		RETURN NEW;
	END;
$$;


ALTER FUNCTION public.log_orders() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 41838)
-- Name: customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customers (
    id integer NOT NULL,
    name text NOT NULL,
    first_name text NOT NULL,
    last_name text NOT NULL,
    phone character varying(17),
    CONSTRAINT customers_first_name_check CHECK ((first_name ~ '^[А-ЯЁ][а-яё]+$'::text)),
    CONSTRAINT customers_last_name_check CHECK ((last_name ~ '^[А-ЯЁ][а-яё]+$'::text)),
    CONSTRAINT customers_name_check CHECK ((name ~ '^[А-ЯЁ][а-яё]+$'::text)),
    CONSTRAINT customers_phone_check CHECK (((phone)::text ~ '^(\+7|8)\(?[0-9]{3}\)?-?[0-9]{3}-?[0-9]{2}-?[0-9]{2}$'::text))
);


ALTER TABLE public.customers OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 41847)
-- Name: customers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.customers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.customers_id_seq OWNER TO postgres;

--
-- TOC entry 4986 (class 0 OID 0)
-- Dependencies: 216
-- Name: customers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.id;


--
-- TOC entry 234 (class 1259 OID 42001)
-- Name: logs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.logs (
    id integer NOT NULL,
    time_stamp timestamp without time zone DEFAULT now() NOT NULL,
    info text
);


ALTER TABLE public.logs OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 42000)
-- Name: logs_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.logs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.logs_id_seq OWNER TO postgres;

--
-- TOC entry 4987 (class 0 OID 0)
-- Dependencies: 233
-- Name: logs_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.logs_id_seq OWNED BY public.logs.id;


--
-- TOC entry 217 (class 1259 OID 41848)
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    id bigint NOT NULL,
    customer_id integer NOT NULL,
    worker_inn character varying(12) NOT NULL,
    work_id smallint NOT NULL,
    descript text,
    receipt_date date DEFAULT now() NOT NULL,
    completion_date date,
    statuse_id smallint NOT NULL,
    payment_type_id smallint NOT NULL,
    CONSTRAINT orders_worker_inn_check CHECK (((worker_inn)::text ~ '^[0-9]{12}$'::text))
);


ALTER TABLE public.orders OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 41855)
-- Name: payment_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.payment_types (
    id smallint NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.payment_types OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 41860)
-- Name: workers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.workers (
    inn character varying(12) NOT NULL,
    name text NOT NULL,
    first_name text NOT NULL,
    last_name text NOT NULL,
    phone character varying(17),
    CONSTRAINT workers_first_name_check CHECK ((first_name ~ '^[А-ЯЁ][а-яё]+$'::text)),
    CONSTRAINT workers_inn_check CHECK (((inn)::text ~ '^[0-9]{12}$'::text)),
    CONSTRAINT workers_last_name_check CHECK ((last_name ~ '^[А-ЯЁ][а-яё]+$'::text)),
    CONSTRAINT workers_name_check CHECK ((name ~ '^[А-ЯЁ][а-яё]+$'::text)),
    CONSTRAINT workers_phone_check CHECK (((phone)::text ~ '^(\+7|8)\(?[0-9]{3}\)?-?[0-9]{3}-?[0-9]{2}-?[0-9]{2}$'::text))
);


ALTER TABLE public.workers OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 41870)
-- Name: works; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.works (
    id smallint NOT NULL,
    name text NOT NULL,
    descript text,
    cost money DEFAULT 0 NOT NULL,
    is_deleted boolean DEFAULT false
);


ALTER TABLE public.works OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 41876)
-- Name: order_receipt; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.order_receipt AS
 SELECT o.id AS "номер_заказа",
    c.first_name AS "фамилия_плательщика",
    c.name AS "имя_плательщика",
    c.last_name AS "отчество_плательщика",
    works.cost AS "стоимость",
    p.name AS "тип_оплаты",
    o.worker_inn AS "ИНН_получателя",
    w.first_name AS "фамилия_получателя",
    w.name AS "имя_получателя",
    w.last_name AS "отчество_получателя"
   FROM ((((public.orders o
     JOIN public.customers c ON ((o.customer_id = c.id)))
     JOIN public.works ON ((o.work_id = works.id)))
     JOIN public.payment_types p ON ((o.payment_type_id = p.id)))
     JOIN public.workers w ON (((o.worker_inn)::text = (w.inn)::text)));


ALTER VIEW public.order_receipt OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 41881)
-- Name: order_reports; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.order_reports AS
 SELECT o.id AS "номер_заказа",
    c.first_name AS "фамилия_заказчика",
    c.name AS "имя_заказчика",
    c.last_name AS "отчество_заказчика",
    works.name AS "работа",
    o.descript AS "описание",
    works.cost AS "стоимость",
    o.receipt_date AS "дата_поступления",
    o.completion_date AS "дата_выполнения",
    w.first_name AS "фамилия_исполнителя",
    w.name AS "имя_исполнителя",
    w.last_name AS "отчество_исполнителя"
   FROM (((public.orders o
     JOIN public.customers c ON ((o.customer_id = c.id)))
     JOIN public.works ON ((o.work_id = works.id)))
     JOIN public.workers w ON (((o.worker_inn)::text = (w.inn)::text)));


ALTER VIEW public.order_reports OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 41886)
-- Name: order_statuses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.order_statuses (
    id smallint NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.order_statuses OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 41891)
-- Name: order_statuses_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.order_statuses_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.order_statuses_id_seq OWNER TO postgres;

--
-- TOC entry 4988 (class 0 OID 0)
-- Dependencies: 224
-- Name: order_statuses_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.order_statuses_id_seq OWNED BY public.order_statuses.id;


--
-- TOC entry 225 (class 1259 OID 41892)
-- Name: orders_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.orders_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.orders_id_seq OWNER TO postgres;

--
-- TOC entry 4989 (class 0 OID 0)
-- Dependencies: 225
-- Name: orders_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;


--
-- TOC entry 226 (class 1259 OID 41893)
-- Name: payment_types_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.payment_types_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.payment_types_id_seq OWNER TO postgres;

--
-- TOC entry 4990 (class 0 OID 0)
-- Dependencies: 226
-- Name: payment_types_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.payment_types_id_seq OWNED BY public.payment_types.id;


--
-- TOC entry 227 (class 1259 OID 41894)
-- Name: period_reports; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.period_reports AS
 SELECT o.id AS "номер_заказа",
    o.worker_inn AS "ИНН_исполнителя",
    w.first_name AS "фамилия_исполнителя",
    w.name AS "имя_исполнителя",
    w.last_name AS "отчество_исполнителя",
    o.completion_date AS "дата_выполнения",
    works.name AS "работа"
   FROM ((public.orders o
     JOIN public.workers w ON (((o.worker_inn)::text = (w.inn)::text)))
     JOIN public.works ON ((o.work_id = works.id)))
  WHERE (o.statuse_id = 2);


ALTER VIEW public.period_reports OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 41899)
-- Name: specializations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.specializations (
    id smallint NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.specializations OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 41904)
-- Name: specializations_for_workers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.specializations_for_workers (
    specialization_id smallint NOT NULL,
    inn character varying(12) NOT NULL,
    CONSTRAINT specializations_for_workers_inn_check CHECK (((inn)::text ~ '^[0-9]{12}$'::text))
);


ALTER TABLE public.specializations_for_workers OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 41908)
-- Name: specializations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.specializations_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.specializations_id_seq OWNER TO postgres;

--
-- TOC entry 4991 (class 0 OID 0)
-- Dependencies: 230
-- Name: specializations_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.specializations_id_seq OWNED BY public.specializations.id;


--
-- TOC entry 231 (class 1259 OID 41909)
-- Name: works_for_specializations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.works_for_specializations (
    specialization_id smallint NOT NULL,
    work_id smallint NOT NULL
);


ALTER TABLE public.works_for_specializations OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 41912)
-- Name: works_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.works_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.works_id_seq OWNER TO postgres;

--
-- TOC entry 4992 (class 0 OID 0)
-- Dependencies: 232
-- Name: works_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.works_id_seq OWNED BY public.works.id;


--
-- TOC entry 4749 (class 2604 OID 41913)
-- Name: customers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers ALTER COLUMN id SET DEFAULT nextval('public.customers_id_seq'::regclass);


--
-- TOC entry 4758 (class 2604 OID 42004)
-- Name: logs id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.logs ALTER COLUMN id SET DEFAULT nextval('public.logs_id_seq'::regclass);


--
-- TOC entry 4756 (class 2604 OID 41914)
-- Name: order_statuses id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_statuses ALTER COLUMN id SET DEFAULT nextval('public.order_statuses_id_seq'::regclass);


--
-- TOC entry 4750 (class 2604 OID 41915)
-- Name: orders id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT nextval('public.orders_id_seq'::regclass);


--
-- TOC entry 4752 (class 2604 OID 41916)
-- Name: payment_types id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment_types ALTER COLUMN id SET DEFAULT nextval('public.payment_types_id_seq'::regclass);


--
-- TOC entry 4757 (class 2604 OID 41917)
-- Name: specializations id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specializations ALTER COLUMN id SET DEFAULT nextval('public.specializations_id_seq'::regclass);


--
-- TOC entry 4753 (class 2604 OID 41918)
-- Name: works id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.works ALTER COLUMN id SET DEFAULT nextval('public.works_id_seq'::regclass);


--
-- TOC entry 4963 (class 0 OID 41838)
-- Dependencies: 215
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.customers VALUES (1, 'Алексей', 'Захаров', 'Викторович', '+7(964)-628-83-28');
INSERT INTO public.customers VALUES (2, 'Светлана', 'Васильева', 'Павловна', '+7(933)-482-94-90');
INSERT INTO public.customers VALUES (3, 'Николай', 'Дмитриев', 'Васильевич', '+7(937)-289-03-04');
INSERT INTO public.customers VALUES (8, 'Никита', 'Смирнов', 'Олегович', NULL);
INSERT INTO public.customers VALUES (4, 'Ольга', 'Попова', 'Николаевна', '+7(903)-945-54-22');


--
-- TOC entry 4979 (class 0 OID 42001)
-- Dependencies: 234
-- Data for Name: logs; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.logs VALUES (3, '2024-04-22 15:00:22.888562', 'UPDATE customer 8');
INSERT INTO public.logs VALUES (4, '2024-04-22 15:02:46.315105', 'UPDATE order 8');
INSERT INTO public.logs VALUES (5, '2024-04-23 08:25:01.217436', 'UPDATE customer 4');
INSERT INTO public.logs VALUES (6, '2024-04-23 08:26:11.20306', 'UPDATE customer 4');


--
-- TOC entry 4969 (class 0 OID 41886)
-- Dependencies: 223
-- Data for Name: order_statuses; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.order_statuses VALUES (1, 'Выполняется');
INSERT INTO public.order_statuses VALUES (2, 'Выполнен');
INSERT INTO public.order_statuses VALUES (3, 'Отменен');


--
-- TOC entry 4965 (class 0 OID 41848)
-- Dependencies: 217
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.orders VALUES (1, 1, '109283746555', 7, NULL, '2024-02-02', '2024-02-02', 2, 1);
INSERT INTO public.orders VALUES (2, 1, '129402683914', 9, NULL, '2024-02-02', '2024-02-02', 2, 1);
INSERT INTO public.orders VALUES (3, 2, '109283746555', 6, 'Фотосессия на праздничной фотозоне', '2024-03-06', '2024-03-07', 2, 2);
INSERT INTO public.orders VALUES (4, 3, '564738291012', 3, 'Съемка ролика для соцсети', '2024-03-06', '2024-03-06', 2, 2);
INSERT INTO public.orders VALUES (5, 3, '378492615403', 4, NULL, '2024-03-06', '2024-03-09', 2, 2);
INSERT INTO public.orders VALUES (6, 4, '109283746555', 8, NULL, '2024-03-12', NULL, 1, 1);
INSERT INTO public.orders VALUES (8, 8, '378492615403', 4, NULL, '2024-03-25', '2024-03-25', 3, 2);


--
-- TOC entry 4966 (class 0 OID 41855)
-- Dependencies: 218
-- Data for Name: payment_types; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.payment_types VALUES (1, 'Оплата после выполнения заказа');
INSERT INTO public.payment_types VALUES (2, 'Предоплата');


--
-- TOC entry 4973 (class 0 OID 41899)
-- Dependencies: 228
-- Data for Name: specializations; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.specializations VALUES (1, 'Специалист по видеосъемке');
INSERT INTO public.specializations VALUES (2, 'Специалист по фотосъемке');
INSERT INTO public.specializations VALUES (3, 'Специалист по обработке фото');
INSERT INTO public.specializations VALUES (4, 'Специалист по обработке и монтажу видео');
INSERT INTO public.specializations VALUES (5, '');


--
-- TOC entry 4974 (class 0 OID 41904)
-- Dependencies: 229
-- Data for Name: specializations_for_workers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.specializations_for_workers VALUES (5, '183456728901');
INSERT INTO public.specializations_for_workers VALUES (3, '129402683914');
INSERT INTO public.specializations_for_workers VALUES (4, '378492615403');
INSERT INTO public.specializations_for_workers VALUES (1, '564738291012');


--
-- TOC entry 4967 (class 0 OID 41860)
-- Dependencies: 219
-- Data for Name: workers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.workers VALUES ('109283746555', 'Кирилл', 'Соколов', 'Витальевич', '+7(927)-845-12-36');
INSERT INTO public.workers VALUES ('129402683914', 'Даниил', 'Денисов', 'Александрович', '+7(923)-374-88-25');
INSERT INTO public.workers VALUES ('183456728901', 'Петр', 'Петров', 'Сергеевич', '+7(921)-345-67-89');
INSERT INTO public.workers VALUES ('564738291012', 'Иван', 'Сидоров', 'Владимирович', '+7(917)-456-32-18');
INSERT INTO public.workers VALUES ('378492615403', 'Марина', 'Васильева', 'Петровна', '+7(939)-242-67-44');


--
-- TOC entry 4968 (class 0 OID 41870)
-- Dependencies: 220
-- Data for Name: works; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.works VALUES (1, 'Ремонт камеры', 'Диагностика и ремонт видеокамеры (Цена может измениться в зависимости от сложности на усмотрение исполнителя)', '5 000,00 ?', false);
INSERT INTO public.works VALUES (2, 'Ремонт фотоаппарата', 'Диагностика и ремонт фотоаппарата (Цена может измениться в зависимости от сложности на усмотрение исполнителя)', '5 000,00 ?', false);
INSERT INTO public.works VALUES (3, 'Съемка видео', 'Запись видео на профессиональную видеокамеру (Цена указана за час работы)', '1 500,00 ?', false);
INSERT INTO public.works VALUES (4, 'Монтаж видео', 'Монтаж видео в профессиональной программе (Цена может измениться в зависимости от сложности на усмотрение исполнителя)', '1 500,00 ?', false);
INSERT INTO public.works VALUES (5, 'Обработка видео', 'Стандартная обработка видео ', '500,00 ?', false);
INSERT INTO public.works VALUES (6, 'Фотосессия', 'Фотосессия (Цена указана за час работы)', '1 500,00 ?', false);
INSERT INTO public.works VALUES (7, 'Фото для документов', 'Съемка фото для документов', '300,00 ?', false);
INSERT INTO public.works VALUES (8, 'Фото', 'Съемка фото', '200,00 ?', false);
INSERT INTO public.works VALUES (9, 'Печать', 'Печать фото', '100,00 ?', false);
INSERT INTO public.works VALUES (10, 'Обработка фото', 'Стандартная обработка фото', '300,00 ?', false);


--
-- TOC entry 4976 (class 0 OID 41909)
-- Dependencies: 231
-- Data for Name: works_for_specializations; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.works_for_specializations VALUES (5, 1);
INSERT INTO public.works_for_specializations VALUES (4, 5);
INSERT INTO public.works_for_specializations VALUES (4, 4);
INSERT INTO public.works_for_specializations VALUES (3, 9);
INSERT INTO public.works_for_specializations VALUES (3, 10);
INSERT INTO public.works_for_specializations VALUES (2, 8);
INSERT INTO public.works_for_specializations VALUES (2, 7);
INSERT INTO public.works_for_specializations VALUES (2, 6);
INSERT INTO public.works_for_specializations VALUES (1, 3);
INSERT INTO public.works_for_specializations VALUES (5, 2);


--
-- TOC entry 4993 (class 0 OID 0)
-- Dependencies: 216
-- Name: customers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.customers_id_seq', 8, true);


--
-- TOC entry 4994 (class 0 OID 0)
-- Dependencies: 233
-- Name: logs_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.logs_id_seq', 6, true);


--
-- TOC entry 4995 (class 0 OID 0)
-- Dependencies: 224
-- Name: order_statuses_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_statuses_id_seq', 4, true);


--
-- TOC entry 4996 (class 0 OID 0)
-- Dependencies: 225
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orders_id_seq', 8, true);


--
-- TOC entry 4997 (class 0 OID 0)
-- Dependencies: 226
-- Name: payment_types_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.payment_types_id_seq', 3, true);


--
-- TOC entry 4998 (class 0 OID 0)
-- Dependencies: 230
-- Name: specializations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.specializations_id_seq', 6, true);


--
-- TOC entry 4999 (class 0 OID 0)
-- Dependencies: 232
-- Name: works_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.works_id_seq', 14, true);


--
-- TOC entry 4774 (class 2606 OID 41920)
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);


--
-- TOC entry 4802 (class 2606 OID 42009)
-- Name: logs logs_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.logs
    ADD CONSTRAINT logs_pkey PRIMARY KEY (id);


--
-- TOC entry 4793 (class 2606 OID 41922)
-- Name: order_statuses order_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_statuses
    ADD CONSTRAINT order_statuses_pkey PRIMARY KEY (id);


--
-- TOC entry 4777 (class 2606 OID 41924)
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- TOC entry 4782 (class 2606 OID 41926)
-- Name: payment_types payment_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment_types
    ADD CONSTRAINT payment_types_pkey PRIMARY KEY (id);


--
-- TOC entry 4798 (class 2606 OID 41928)
-- Name: specializations specializations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specializations
    ADD CONSTRAINT specializations_pkey PRIMARY KEY (id);


--
-- TOC entry 4786 (class 2606 OID 41930)
-- Name: workers workers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_pkey PRIMARY KEY (inn);


--
-- TOC entry 4800 (class 2606 OID 41932)
-- Name: works_for_specializations works_for_specializations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.works_for_specializations
    ADD CONSTRAINT works_for_specializations_pkey PRIMARY KEY (work_id);


--
-- TOC entry 4790 (class 2606 OID 41934)
-- Name: works works_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.works
    ADD CONSTRAINT works_pkey PRIMARY KEY (id);


--
-- TOC entry 4771 (class 1259 OID 41935)
-- Name: customers_first_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX customers_first_name_index ON public.customers USING btree (first_name);


--
-- TOC entry 4772 (class 1259 OID 41936)
-- Name: customers_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX customers_idx ON public.customers USING btree (id);

ALTER TABLE public.customers CLUSTER ON customers_idx;


--
-- TOC entry 4791 (class 1259 OID 41937)
-- Name: order_statuses_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX order_statuses_idx ON public.order_statuses USING btree (id);

ALTER TABLE public.order_statuses CLUSTER ON order_statuses_idx;


--
-- TOC entry 4775 (class 1259 OID 41938)
-- Name: orders_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX orders_idx ON public.orders USING btree (id);

ALTER TABLE public.orders CLUSTER ON orders_idx;


--
-- TOC entry 4778 (class 1259 OID 41939)
-- Name: orders_receipt_date_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX orders_receipt_date_index ON public.orders USING btree (receipt_date);


--
-- TOC entry 4779 (class 1259 OID 41940)
-- Name: payment_type_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX payment_type_name_index ON public.payment_types USING btree (name);


--
-- TOC entry 4780 (class 1259 OID 41941)
-- Name: payment_types_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX payment_types_idx ON public.payment_types USING btree (id);

ALTER TABLE public.payment_types CLUSTER ON payment_types_idx;


--
-- TOC entry 4795 (class 1259 OID 41942)
-- Name: specializations_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX specializations_idx ON public.specializations USING btree (id);

ALTER TABLE public.specializations CLUSTER ON specializations_idx;


--
-- TOC entry 4796 (class 1259 OID 41943)
-- Name: specializations_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX specializations_name_index ON public.specializations USING btree (name);


--
-- TOC entry 4794 (class 1259 OID 41944)
-- Name: statuses_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX statuses_name_index ON public.order_statuses USING btree (name);


--
-- TOC entry 4783 (class 1259 OID 41945)
-- Name: workers_first_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX workers_first_name_index ON public.workers USING btree (first_name);


--
-- TOC entry 4784 (class 1259 OID 41946)
-- Name: workers_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX workers_idx ON public.workers USING btree (inn);

ALTER TABLE public.workers CLUSTER ON workers_idx;


--
-- TOC entry 4787 (class 1259 OID 41947)
-- Name: works_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX works_idx ON public.works USING btree (id);

ALTER TABLE public.works CLUSTER ON works_idx;


--
-- TOC entry 4788 (class 1259 OID 41948)
-- Name: works_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX works_name_index ON public.works USING btree (name);


--
-- TOC entry 4814 (class 2620 OID 41999)
-- Name: workers before_delete_from_workers; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER before_delete_from_workers BEFORE DELETE ON public.workers FOR EACH ROW EXECUTE FUNCTION public.before_delete_from_workers();


--
-- TOC entry 4815 (class 2620 OID 41996)
-- Name: workers check_inn; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER check_inn BEFORE INSERT OR UPDATE ON public.workers FOR EACH ROW EXECUTE FUNCTION public.check_inn();


--
-- TOC entry 4816 (class 2620 OID 50187)
-- Name: works delete_work; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER delete_work BEFORE DELETE ON public.works FOR EACH ROW EXECUTE FUNCTION public.delete_work();


--
-- TOC entry 4812 (class 2620 OID 42013)
-- Name: customers log_customers; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER log_customers AFTER UPDATE ON public.customers FOR EACH ROW EXECUTE FUNCTION public.log_customers();


--
-- TOC entry 4813 (class 2620 OID 42015)
-- Name: orders log_orders; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER log_orders AFTER UPDATE ON public.orders FOR EACH ROW EXECUTE FUNCTION public.log_orders();


--
-- TOC entry 4803 (class 2606 OID 41949)
-- Name: orders orders_customer_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(id);


--
-- TOC entry 4804 (class 2606 OID 41954)
-- Name: orders orders_payment_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_payment_type_id_fkey FOREIGN KEY (payment_type_id) REFERENCES public.payment_types(id);


--
-- TOC entry 4805 (class 2606 OID 41959)
-- Name: orders orders_statuse_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_statuse_id_fkey FOREIGN KEY (statuse_id) REFERENCES public.order_statuses(id);


--
-- TOC entry 4806 (class 2606 OID 41964)
-- Name: orders orders_work_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_work_id_fkey FOREIGN KEY (work_id) REFERENCES public.works(id);


--
-- TOC entry 4807 (class 2606 OID 41969)
-- Name: orders orders_worker_inn_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_worker_inn_fkey FOREIGN KEY (worker_inn) REFERENCES public.workers(inn);


--
-- TOC entry 4808 (class 2606 OID 41974)
-- Name: specializations_for_workers specializations_for_workers_inn_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specializations_for_workers
    ADD CONSTRAINT specializations_for_workers_inn_fkey FOREIGN KEY (inn) REFERENCES public.workers(inn);


--
-- TOC entry 4809 (class 2606 OID 41979)
-- Name: specializations_for_workers specializations_for_workers_specialization_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specializations_for_workers
    ADD CONSTRAINT specializations_for_workers_specialization_id_fkey FOREIGN KEY (specialization_id) REFERENCES public.specializations(id);


--
-- TOC entry 4810 (class 2606 OID 41984)
-- Name: works_for_specializations works_for_specializations_specialization_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.works_for_specializations
    ADD CONSTRAINT works_for_specializations_specialization_id_fkey FOREIGN KEY (specialization_id) REFERENCES public.specializations(id);


--
-- TOC entry 4811 (class 2606 OID 41989)
-- Name: works_for_specializations works_for_specializations_work_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.works_for_specializations
    ADD CONSTRAINT works_for_specializations_work_id_fkey FOREIGN KEY (work_id) REFERENCES public.works(id);


--
-- TOC entry 4748 (class 3466 OID 50192)
-- Name: abort_alter_view; Type: EVENT TRIGGER; Schema: -; Owner: postgres
--

CREATE EVENT TRIGGER abort_alter_view ON ddl_command_start
         WHEN TAG IN ('ALTER VIEW')
   EXECUTE FUNCTION public.abort_alter_view_command();


ALTER EVENT TRIGGER abort_alter_view OWNER TO postgres;

-- Completed on 2024-05-06 01:31:54

--
-- PostgreSQL database dump complete
--

