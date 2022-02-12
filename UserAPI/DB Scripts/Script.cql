CREATE KeySpace test;
CREATE TABLE test.users (
    email text,
    user_id uuid,
    first_name text,
    middle_name text,
    last_name text,
    phone_number text,
    PRIMARY KEY (email));