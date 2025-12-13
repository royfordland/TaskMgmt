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