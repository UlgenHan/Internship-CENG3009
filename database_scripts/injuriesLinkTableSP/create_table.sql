CREATE TABLE INJURESLINK (
    INJURESLINKID NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    INJURYSUSPENSIONID NUMBER NOT NULL,
    SEVERITY NUMBER(1) NOT NULL,
    FOREIGN KEY (INJURYSUSPENSIONID) REFERENCES INJURIESSUSPENSIONSTABLE(INJURYSUSPENSIONID)
);