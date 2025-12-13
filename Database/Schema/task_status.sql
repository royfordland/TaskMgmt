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