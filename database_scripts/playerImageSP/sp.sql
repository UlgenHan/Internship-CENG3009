-- Create Procedure for PlayerImagesTable
CREATE OR REPLACE PROCEDURE player_image_create (
    p_PlayerId NUMBER,
    p_ImageData BLOB
) IS
BEGIN
    INSERT INTO PlayerImagesTable (
        PlayerId, ImageData
    ) VALUES (
        p_PlayerId, p_ImageData
    );
    
    DBMS_OUTPUT.PUT_LINE('Player image created for PlayerId: ' || p_PlayerId);
END;
/

-- Get By ID Procedure for PlayerImagesTable
CREATE OR REPLACE PROCEDURE player_image_getById (
    p_PlayerId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM PlayerImagesTable
    WHERE PlayerId = p_PlayerId;
END;
/

-- Update Procedure for PlayerImagesTable
CREATE OR REPLACE PROCEDURE player_image_update (
    p_PlayerId IN NUMBER,
    p_ImageData BLOB
) IS
BEGIN
    UPDATE PlayerImagesTable
    SET ImageData = p_ImageData
    WHERE PlayerId = p_PlayerId;
END;
/

-- Delete Procedure for PlayerImagesTable
CREATE OR REPLACE PROCEDURE player_image_delete (
    p_PlayerId IN NUMBER
) IS
BEGIN
    DELETE FROM PlayerImagesTable
    WHERE PlayerId = p_PlayerId;
END;
/
