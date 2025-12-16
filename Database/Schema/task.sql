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
	CONSTRAINT task_task_status_fk FOREIGN KEY (status_id) REFERENCES taskmgmt.public.task_status(id)
);

-- Column comments

COMMENT ON COLUMN taskmgmt.public.task.description IS 'Open to requirements, decided to make this optional. Title is required, description could be used in case the title is too long.';