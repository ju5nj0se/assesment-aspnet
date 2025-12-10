INSERT INTO "Degrees" ("Name") VALUES 
('Ingeniero'),
('Soporte Técnico'),
('Analista'),
('Coordinador'),
('Desarrollador'),
('Auxiliar'),
('Administrador'),
('Técnico')
ON CONFLICT DO NOTHING;

INSERT INTO "Departments" ("Name") VALUES 
('Logística'),
('Marketing'),
('Recursos Humanos'),
('Operaciones'),
('Tecnología'),
('Contabilidad'),
('Ventas')
ON CONFLICT DO NOTHING;

INSERT INTO "EducationLevels" ("Name") VALUES 
('Profesional'),
('Tecnólogo'),
('Maestría'),
('Especialización')
ON CONFLICT DO NOTHING;

INSERT INTO "StatusEmployees" ("Status") VALUES 
('Vacaciones'),
('Activo'),
('Inactivo')
ON CONFLICT DO NOTHING;
