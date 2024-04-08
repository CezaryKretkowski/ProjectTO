Drop TABLE Type;
create table if not exists Type (
    id int primary key ,
     Name  varchar(255) not null
);
insert into Type values(1,'Function');
insert into Type values(2,'In');
insert into Type values(3,'Out');
insert into Type values(4,'Uniform');


CREATE TABLE if not exists DataTypes(
    id int primary key ,
    Name  varchar(255) not null
);
insert into DataTypes values(1,'int');
insert into DataTypes values(2,'float');
insert into DataTypes values(3,'double');
insert into DataTypes values(4,'bool');
insert into DataTypes values(5,'vec2');
insert into DataTypes values(6,'vec3');
insert into DataTypes values(7,'vec4');
insert into DataTypes values(8,'ivec2');
insert into DataTypes values(9,'ivec3');
insert into DataTypes values(10,'ivec4');
insert into DataTypes values(11,'bvec2');
insert into DataTypes values(12,'bvec3');
insert into DataTypes values(13,'bvec4');
insert into DataTypes values(14,'mat2');
insert into DataTypes values(15,'mat3');
insert into DataTypes values(16,'mat4');
insert into DataTypes values(17,'mat2x3');
insert into DataTypes values(18,'mat2x4');
insert into DataTypes values(19,'mat3x2');
insert into DataTypes values(20,'mat3x4');
insert into DataTypes values(21,'mat4x2');
insert into DataTypes values(22,'mat4x3');
insert into DataTypes values(23,'sampler1D');
insert into DataTypes values(24,'sampler2D');
insert into DataTypes values(25,'sampler3D');
insert into DataTypes values(26,'samplerCube');
insert into DataTypes values(27,'sampler2DArray');
insert into DataTypes values(28,'samplerCubeArray');
insert into DataTypes values(29,'texture1D');
insert into DataTypes values(30,'texture2D');
insert into DataTypes values(31,'texture3D');
insert into DataTypes values(32,'textureCube');
insert into DataTypes values(33,'texture2DArray');
insert into DataTypes values(34,'textureCubeArray');
insert into DataTypes values(35,'void');

CREATE TABLE Node(
                     id int primary key ,
                     Name varchar(255) not null ,
                     TypeId int not null ,
                     FOREIGN KEY (TypeId) REFERENCES Type(id)
);