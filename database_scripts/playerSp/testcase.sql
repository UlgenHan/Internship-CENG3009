DECLARE
    v_PlayerId NUMBER;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--- Test Case 1: football_playertable_create - Valid Input ---');
    BEGIN
        football_playertable_create('John', 'Doe', 25, TO_DATE('1999-05-15', 'YYYY-MM-DD'), 'US', 'Forward', 'ABC FC', 180, 75, 'Right', v_PlayerId);
        DBMS_OUTPUT.PUT_LINE('Player created with ID: ' || v_PlayerId);
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Error in Test Case 1: ' || SQLERRM);
    END;
END;
/


DECLARE
    v_PlayerId NUMBER;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--- Test Case 2: football_playertable_create - Missing Required Fields ---');
    BEGIN
        football_playertable_create('Alice', NULL, NULL, NULL, NULL, 'Midfielder', 'XYZ FC', NULL, NULL, NULL, v_PlayerId);
        DBMS_OUTPUT.PUT_LINE('Player created with ID: ' || v_PlayerId);
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Error in Test Case 2: ' || SQLERRM);
    END;
END;
/


DECLARE
    v_Cursor SYS_REFCURSOR;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--- Test Case 3: football_playertable_getById - Valid PlayerId ---');
    BEGIN
        football_playertable_getById(8, v_Cursor);
        -- Fetch and process cursor data here if needed
        DBMS_OUTPUT.PUT_LINE('Test Case 3: football_playertable_getById - Valid PlayerId: Successfully fetched data');
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Error in Test Case 3: ' || SQLERRM);
    END;
END;
/

DECLARE
    v_Cursor SYS_REFCURSOR;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--- Test Case 5: football_playertable_getAll ---');
    BEGIN
        football_playertable_getAll(v_Cursor);
        -- Fetch and process cursor data here if needed
        DBMS_OUTPUT.PUT_LINE('Test Case 5: football_playertable_getAll: Successfully fetched all player records');
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Error in Test Case 5: ' || SQLERRM);
    END;
END;
/

DECLARE
    v_Cursor SYS_REFCURSOR;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--- Test Case 6: football_playertable_getByPagination - Valid Pagination ---');
    BEGIN
        football_playertable_getByPagination(10, 1, v_Cursor); -- Assuming 10 records per page, page 1
        -- Fetch and process cursor data here if needed
        DBMS_OUTPUT.PUT_LINE('Test Case 6: football_playertable_getByPagination - Valid Pagination: Successfully fetched paginated records');
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Error in Test Case 6: ' || SQLERRM);
    END;
END;
/


select * from playertable
