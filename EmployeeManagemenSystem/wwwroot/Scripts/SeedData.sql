INSERT INTO "degrees" ("name") VALUES 
('Ingeniero'),
('Soporte Técnico'),
('Analista'),
('Coordinador'),
('Desarrollador'),
('Auxiliar'),
('Administrador')
ON CONFLICT DO NOTHING;

INSERT INTO "departments" ("name") VALUES 
('Logística'),
('Marketing'),
('Recursos Humanos'),
('Operaciones'),
('Tecnología'),
('Contabilidad'),
('Ventas')
ON CONFLICT DO NOTHING;

INSERT INTO "educationlevels" ("name") VALUES 
('Profesional'),
('Tecnólogo'),
('Maestría'),
('Especialización'),
('Técnico')
ON CONFLICT DO NOTHING;

INSERT INTO "statusemployees" ("status") VALUES 
('Vacaciones'),
('Activo'),
('Inactivo')
ON CONFLICT DO NOTHING;