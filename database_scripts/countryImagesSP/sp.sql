-- Create Procedure for CountryFlagsTable
CREATE OR REPLACE PROCEDURE country_flag_create (
    p_CountryCode VARCHAR2,
    p_ImageData BLOB
) IS
BEGIN
    INSERT INTO CountryFlagsTable (
        CountryCode, ImageData
    ) VALUES (
        p_CountryCode, p_ImageData
    );
    
    DBMS_OUTPUT.PUT_LINE('Country flag created for CountryCode: ' || p_CountryCode);
END;
/

-- Get By Code Procedure for CountryFlagsTable
CREATE OR REPLACE PROCEDURE country_flag_getByCode (
    p_CountryCode IN VARCHAR2,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM CountryFlagsTable
    WHERE CountryCode = p_CountryCode;
END;
/

-- Update Procedure for CountryFlagsTable
CREATE OR REPLACE PROCEDURE country_flag_update (
    p_CountryCode IN VARCHAR2,
    p_ImageData BLOB
) IS
BEGIN
    UPDATE CountryFlagsTable
    SET ImageData = p_ImageData
    WHERE CountryCode = p_CountryCode;
END;
/

-- Delete Procedure for CountryFlagsTable
CREATE OR REPLACE PROCEDURE country_flag_delete (
    p_CountryCode IN VARCHAR2
) IS
BEGIN
    DELETE FROM CountryFlagsTable
    WHERE CountryCode = p_CountryCode;
END;
/
