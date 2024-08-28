-- Create Procedure for TeamImagesTable
CREATE OR REPLACE PROCEDURE team_image_create (
    p_TeamId NUMBER,
    p_ImageData BLOB
) IS
BEGIN
    INSERT INTO TeamImagesTable (
        TeamId, ImageData
    ) VALUES (
        p_TeamId, p_ImageData
    );
    
    DBMS_OUTPUT.PUT_LINE('Team image created for TeamId: ' || p_TeamId);
END;
/

-- Get By ID Procedure for TeamImagesTable
CREATE OR REPLACE PROCEDURE team_image_getById (
    p_TeamId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM TeamImagesTable
    WHERE TeamId = p_TeamId;
END;
/

-- Update Procedure for TeamImagesTable
CREATE OR REPLACE PROCEDURE team_image_update (
    p_TeamId IN NUMBER,
    p_ImageData BLOB
) IS
BEGIN
    UPDATE TeamImagesTable
    SET ImageData = p_ImageData
    WHERE TeamId = p_TeamId;
END;
/

-- Delete Procedure for TeamImagesTable
CREATE OR REPLACE PROCEDURE team_image_delete (
    p_TeamId IN NUMBER
) IS
BEGIN
    DELETE FROM TeamImagesTable
    WHERE TeamId = p_TeamId;
END;
/
