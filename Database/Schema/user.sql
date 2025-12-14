CREATE TABLE taskmgmt.public."user" (
	id bigint GENERATED ALWAYS AS IDENTITY NOT NULL,
	username varchar NOT NULL,
	email varchar NULL,
	password text NOT NULL,
	is_admin bool DEFAULT FALSE NOT NULL,
	is_active bool DEFAULT TRUE NOT NULL,
	created_dt timestamp DEFAULT NOW() NOT NULL,
	created_by bigint NULL,
	updated_dt timestamp NULL,
	updated_by bigint NULL,
	CONSTRAINT user_pk PRIMARY KEY (id)
);

-- Column comments

COMMENT ON COLUMN taskmgmt.public."user".created_by IS 'The assignment doesn''t specify if users can only register themselves or if they can be created by admin users or not. Admin / role based code is a bonus, but just as a note I would like to mention that depending on the requirements this column could be optional or required. An empty value currently implies that the user themselves created this record, and there''s no way to use the id that is generated in the same insert. At the same time I want to avoid having to do an UPDATE right after.';