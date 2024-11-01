CREATE TABLE CODES_MASTER (
    CM_CODE VARCHAR2(12) NOT NULL,
    CM_TYPE VARCHAR2(12) NOT NULL,
    CM_DESC VARCHAR2(240),
    CM_VALUE NUMBER(9,2),
    CM_CR_BY VARCHAR2(12),
    CM_CR_DT DATE,
    CM_UP_BY VARCHAR2(12),
    CM_UP_DT DATE,
    CM_ACTIVE_YN CHAR(1) DEFAULT 'Y',
    CONSTRAINT PK_CODES_MASTER PRIMARY KEY (CM_CODE, CM_TYPE)
);

CREATE TABLE ERROR_CODE_MASTER (
    ERR_CODE VARCHAR2(12) NOT NULL,
    ERR_TYPE VARCHAR2(12),
    ERR_DESC VARCHAR2(240),
    ERR_CR_BY VARCHAR2(12),
    ERR_CR_DT DATE,
    ERR_UP_BY VARCHAR2(12),
    ERR_UP_DT DATE,
    CONSTRAINT PK_ERROR_CODE_MASTER PRIMARY KEY (ERR_CODE)
);

CREATE TABLE USER_MASTER (
    USER_ID VARCHAR2(12) NOT NULL,
    USER_NAME VARCHAR2(30),
    USER_PASSWORD VARCHAR2(24),
    USER_TYPE VARCHAR2(1),
    USER_CR_BY VARCHAR2(12),
    USER_CR_DT DATE,
    USER_UP_BY VARCHAR2(12),
    USER_UP_DT DATE,
    CONSTRAINT PK_USER_MASTER PRIMARY KEY (USER_ID)
);

CREATE TABLE MOTOR_CLAIM (
    CLM_UID NUMBER NOT NULL,
    CLM_NO VARCHAR2(30),
    CLM_POL_NO VARCHAR2(30),
    CLM_POL_FM_DT DATE,
    CLM_POL_TO_DT DATE,
    CLM_LOSS_DT DATE,
    CLM_INTM_DT DATE,
    CLM_REG_DT DATE,
    CLM_POL_REP_NO VARCHAR2(30),
    CLM_LOSS_DESC VARCHAR2(120),
    CLM_VEH_CHASSIS_NO VARCHAR2(30),
    CLM_VEH_ENGINE_NO VARCHAR2(30),
    CLM_VEH_REGN_NO VARCHAR2(12),
    CLM_VEH_VALUE NUMBER(9,2),
    CLM_SUR_CR_YN VARCHAR2(1),
    CLM_SUR_APPR_YN VARCHAR2(1),
    CLM_SUR_NO VARCHAR2(30),
    CLM_CR_BY VARCHAR2(12),
    CLM_CR_DT DATE,
    CLM_UP_BY VARCHAR2(12),
    CLM_UP_DT DATE,
    CONSTRAINT PK_MOTOR_CLAIM PRIMARY KEY (CLM_UID)
);

CREATE TABLE MOTOR_CLM_SUR_HDR (
    SUR_UID NUMBER NOT NULL,
    SUR_CLM_UID NUMBER,
    SUR_CLM_NO VARCHAR2(30),
    SUR_NO VARCHAR2(30),
    SUR_CHASSIS_NO VARCHAR2(30),
    SUR_REGN_NO VARCHAR2(12),
    SUR_ENGINE_NO VARCHAR2(24),
    SUR_CURR VARCHAR2(12),
    SUR_FC_AMT NUMBER(9,2),
    SUR_LC_AMT NUMBER(9,2),
    SUR_STATUS VARCHAR2(1),
    SUR_APPR_STS VARCHAR2(1),
    SUR_APPR_DT DATE,
    SUR_APPR_BY VARCHAR2(12),
    SUR_CR_BY VARCHAR2(12),
    SUR_CR_DT DATE,
    SUR_UP_BY VARCHAR2(12),
    SUR_UP_DT DATE,
    CONSTRAINT PK_MOTOR_CLM_SUR_HDR PRIMARY KEY (SUR_UID)
);

CREATE TABLE MOTOR_CLM_SUR_DTL (
    SURD_UID NUMBER NOT NULL,
    SURD_SUR_UID NUMBER,
    SURD_ITEM_CODE VARCHAR2(12),
    SURD_TYPE VARCHAR2(12),
    SURD_UNIT_PRICE NUMBER(9,2),
    SURD_QUANTITY NUMBER,
    SURD_FC_AMT NUMBER(9,2),
    SURD_LC_AMT NUMBER(9,2),
    SURD_CR_BY VARCHAR2(12),
    SURD_CR_DT DATE,
    SURD_UP_BY VARCHAR2(12),
    SURD_UP_DT DATE,
    CONSTRAINT PK_MOTOR_CLM_SUR_DTL PRIMARY KEY (SURD_UID)
);

CREATE TABLE MOTOR_CLM_SUR_DTL_HIST (
    SURDH_UID NUMBER NOT NULL,
    SURDH_SRL NUMBER NOT NULL,
    SURDH_ACTION VARCHAR2(1),
    SURDH_SUR_UID NUMBER,
    SURDH_ITEM_CODE VARCHAR2(12),
    SURDH_TYPE VARCHAR2(12),
    SURDH_UNIT_PRICE NUMBER(9,2),
    SURDH_QUANTITY NUMBER,
    SURDH_FC_AMT NUMBER(9,2),
    SURDH_LC_AMT NUMBER(9,2),
    SURDH_CR_BY VARCHAR2(12),
    SURDH_CR_DT DATE,
    SURDH_UP_BY VARCHAR2(12),
    SURDH_UP_DT DATE,
    CONSTRAINT PK_MOTOR_CLM_SUR_DTL_HIST PRIMARY KEY (SURDH_UID, SURDH_SRL)
);

