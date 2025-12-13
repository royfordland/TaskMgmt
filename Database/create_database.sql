-- Note; database, tables, etc were created using DBeaver, the following steps were used to execute this file
-- 1. Run the create database line.
-- 2. Set the new database as the default / active database.
-- 3. Run the remaining code by "Execute SQL Script" (or whatever the variant in your SQL tool of prefence is that isn't just a regular query)
-- The following exists when you run the psql shell
-- \c taskmgmt
-- Which is supposed to set the connection to the new database, I used the steps described above instead.

-- Step 1: Create database
CREATE DATABASE taskmgmt;

-- Step 2: Set new database as default, then create the tables, roles, etc.
CREATE TABLE taskmgmt.public."user" (
	id bigint GENERATED ALWAYS AS IDENTITY NOT NULL,
	username varchar NOT NULL,
	email varchar NULL,
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

CREATE TABLE taskmgmt.public.task_status (
	id bigint GENERATED ALWAYS AS IDENTITY NOT NULL,
	status varchar NULL,
	is_active bool DEFAULT true NOT NULL,
	created_dt timestamp DEFAULT NOW() NOT NULL,
	created_by bigint NULL,
	updated_dt timestamp NULL,
	updated_by bigint NULL,
	CONSTRAINT task_status_pk PRIMARY KEY (id)
);

-- Column comments

COMMENT ON COLUMN taskmgmt.public.task_status.created_by IS 'There are some default records that are part of scripts to create this database, but any admin actions that add / update any records should have a user id attached to it that would be stored in this column';

INSERT INTO taskmgmt.public.task_status (status)
VALUES
('Pending'),
('In Progress'),
('Completed');

CREATE TABLE taskmgmt.public.task (
	id bigint GENERATED ALWAYS AS IDENTITY NOT NULL,
	title varchar NOT NULL,
	description text NULL,
	status_id bigint NOT NULL,
	assigned_user_id bigint NULL,
	created_by bigint NOT NULL,
	created_dt timestamp DEFAULT NOW() NOT NULL,
	updated_by bigint NULL,
	updated_dt timestamp NULL,
	CONSTRAINT task_pk PRIMARY KEY (id),
	CONSTRAINT task_task_status_fk FOREIGN KEY (id) REFERENCES taskmgmt.public.task_status(id),
	CONSTRAINT task_user_fk FOREIGN KEY (assigned_user_id) REFERENCES taskmgmt.public.user(id)
);

-- Column comments

COMMENT ON COLUMN taskmgmt.public.task.description IS 'Open to requirements, decided to make this optional. Title is required, description could be used in case the title is too long.';

CREATE ROLE "taskMgmtUser" NOSUPERUSER NOCREATEDB NOCREATEROLE NOINHERIT LOGIN NOREPLICATION NOBYPASSRLS PASSWORD 'tmu123';
GRANT DELETE, SELECT, INSERT, UPDATE ON TABLE taskmgmt.public.task TO "taskMgmtUser";
GRANT DELETE, SELECT, INSERT, UPDATE ON TABLE taskmgmt.public.task_status TO "taskMgmtUser";
GRANT DELETE, SELECT, INSERT, UPDATE ON TABLE taskmgmt.public."user" TO "taskMgmtUser";
