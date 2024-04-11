Drop TABLE Type;
create table if not exists Type (
    id int primary key ,
     Name  varchar(255) not null
);
insert into Type values(1,'Function');
insert into Type values(2,'In');
insert into Type values(3,'Out');
insert into Type values(4,'Uniform');

drop table DataTypes;
CREATE TABLE if not exists DataTypes(
    id int primary key ,
    Name  varchar(255) not null,
    _Ctype  varchar(255) not null
);
insert into DataTypes values(1,'int','int');
insert into DataTypes values(2,'float','float');
insert into DataTypes values(3,'double','double');
insert into DataTypes values(4,'bool','bool');
insert into DataTypes values(5,'vec2','Vector2');
insert into DataTypes values(6,'vec3','Vector3');
insert into DataTypes values(7,'vec4','Vector4');
insert into DataTypes values(8,'ivec2,','Vector2');
insert into DataTypes values(9,'ivec3','Vector3');
insert into DataTypes values(10,'ivec4','Vector4');
insert into DataTypes values(11,'bvec2','Vector2');
insert into DataTypes values(12,'bvec3','Vector3');
insert into DataTypes values(13,'bvec4','Vector2');
insert into DataTypes values(14,'mat2','none');
insert into DataTypes values(15,'mat3','none');
insert into DataTypes values(16,'mat4','none');
insert into DataTypes values(17,'mat2x3','none');
insert into DataTypes values(18,'mat2x4','none');
insert into DataTypes values(19,'mat3x2','none');
insert into DataTypes values(20,'mat3x4','none');
insert into DataTypes values(21,'mat4x2','none');
insert into DataTypes values(22,'mat4x3','none');
insert into DataTypes values(23,'sampler1D','none');
insert into DataTypes values(24,'sampler2D','none');
insert into DataTypes values(25,'sampler3D','none');
insert into DataTypes values(26,'samplerCube','none');
insert into DataTypes values(27,'sampler2DArray','none');
insert into DataTypes values(28,'samplerCubeArray','none');
insert into DataTypes values(29,'texture1D','none');
insert into DataTypes values(30,'texture2D','none');
insert into DataTypes values(31,'texture3D','none');
insert into DataTypes values(32,'textureCube','none');
insert into DataTypes values(33,'texture2DArray','none');
insert into DataTypes values(34,'textureCubeArray','none');
insert into DataTypes values(35,'void','none');

CREATE TABLE Node(
            id int primary key ,
            Name varchar(255) not null ,
            TypeId int not null ,
            FOREIGN KEY (TypeId) REFERENCES Type(id)
);


Create Table Input(
       id int primary key ,
       Name varchar(255) not null ,
       DefaultValue  varchar(255),
       DataTypesId int not null ,
       NodeId int not null ,
       FOREIGN KEY (DataTypesId) REFERENCES DataTypes(id),
       FOREIGN KEY (NodeId) REFERENCES Node(id)     
);

insert into Node values(1,'GL_Position',1);
INSERT into Input values(1,'GL_Pos_VEC3','','6', '1');
INSERT into Input values(2,'GL_Pos_W_Float','1.0f',6, 1);