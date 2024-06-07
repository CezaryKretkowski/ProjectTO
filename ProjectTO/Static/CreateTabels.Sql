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
    _Ctype  varchar(255) not null,
    InpautInterface varchar(255) not Null
);

insert into DataTypes values(1,'int','int','ProjectTo.Modules.InputManager.Implementation.IntInputHandler');
insert into DataTypes values(2,'float','float','ProjectTo.Modules.InputManager.Implementation.FloatInputHandler');
insert into DataTypes values(3,'double','double','ProjectTo.Modules.InputManager.Implementation.FloatInputHandler');
insert into DataTypes values(4,'bool','bool','ProjectTo.Modules.InputManager.Implementation.IntInputHandler');
insert into DataTypes values(5,'vec2','Vector2','ProjectTo.Modules.InputManager.Implementation.Vec2InputHandler');
insert into DataTypes values(6,'vec3','Vector3','ProjectTo.Modules.InputManager.Implementation.Vec3InputHandler');
insert into DataTypes values(7,'vec4','Vector4','ProjectTo.Modules.InputManager.Implementation.Vec4InputHandler');
-- insert into DataTypes values(8,'ivec2,','Vector2');
-- insert into DataTypes values(9,'ivec3','Vector3');
-- insert into DataTypes values(10,'ivec4','Vector4');
-- insert into DataTypes values(11,'bvec2','Vector2');
-- insert into DataTypes values(12,'bvec3','Vector3');
-- insert into DataTypes values(13,'bvec4','Vector2');
-- insert into DataTypes values(14,'mat2','none');
-- insert into DataTypes values(15,'mat3','none');
insert into DataTypes values(16,'mat4','none','ProjectTo.Modules.InputManager.Implementation.Mat4Handler');
-- insert into DataTypes values(17,'mat2x3','none');
-- insert into DataTypes values(18,'mat2x4','none');
-- insert into DataTypes values(19,'mat3x2','none');
-- insert into DataTypes values(20,'mat3x4','none');
-- insert into DataTypes values(21,'mat4x2','none');
-- insert into DataTypes values(22,'mat4x3','none');
-- insert into DataTypes values(23,'sampler1D','none');
insert into DataTypes values(24,'sampler2D','none','ProjectTo.Modules.InputManager.Implementation.TwoDSamplerInputHandler');
-- insert into DataTypes values(25,'sampler3D','none');
-- insert into DataTypes values(26,'samplerCube','none');
-- insert into DataTypes values(27,'sampler2DArray','none');
-- insert into DataTypes values(28,'samplerCubeArray','none');
-- insert into DataTypes values(29,'texture1D','none');
-- insert into DataTypes values(30,'texture2D','none');
-- insert into DataTypes values(31,'texture3D','none');
-- insert into DataTypes values(32,'textureCube','none');
-- insert into DataTypes values(33,'texture2DArray','none');
-- insert into DataTypes values(34,'textureCubeArray','none');
-- insert into DataTypes values(35,'void','none');

Drop table Node;
CREATE TABLE Node(
            id int primary key ,
            Name varchar(255) not null ,
            TypeId int not null ,
            ToStringFormat varchar(1000),
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


DROP TABLE Input;
delete from input;
-- gl position
insert into Node values(1,'gl_position',1,'{0};');
INSERT into Input values(1,'GL_Pos_VEC4','',7, 1);
-- cast vec3 to vec4
insert into Node values(2,'ToVector4',1,'vec4({0},{1});');
INSERT into Input values(3,'GL_Pos_VEC3','',6, 2);
INSERT into Input values(4,'GL_Pos_W_Float','1.0f',2, 2);
INSERT into Output values(1,'OutToVec4',7, 2);
-- multyplay three mat4
insert into Node values(3,'Multi3Mat4',1,'{0}*{1}*{2};');
INSERT into Input values(5,'MulMat4_0','',16, 3);
INSERT into Input values(6,'MulMat4_1','',16, 3);
INSERT into Input values(7,'MulMat4_2','',16, 3);
INSERT into Output values(2,'OutToMul3Mat4',16, 3);
-- multiplat vec4 by mat4
insert into Node values(4,'MultiVec4Mat4',1,'{0}*{1};');
INSERT into Input values(8,'MulVec4_0','',7, 4);
INSERT into Input values(9,'MulMat4_1','',16, 4);
INSERT into Output values(3,'OutToMulVec4Mat4',7, 4);

--texture()
insert into Node values(5,'Texture',1,'vec4({0},{1});');
INSERT into Input values(10,'samplerTexture','',24, 5);
INSERT into Input values(11,'Uv_map','',5, 5);
INSERT into Output values(4,'OutToVec4',7, 5);
---LIGHT-----------------------
    
    
--normalize --- FRAG SHADER
insert into Node values(6,'Normalize',1,'normalize({0});');
INSERT into Input values(12,'Vec3_0','',6, 6);
INSERT into Output values(5,'OutToVecNormalize',6, 6);

--multipaly Mat4xVec4 -VERTEX SHADER
insert into Node values(7,'MultiMAt4Vec3',1,'vec3({1}*{0});');
INSERT into Input values(13,'Mat4_0','',16, 7);
INSERT into Input values(14,'Vec4_0','',7, 7);
INSERT into Output values(6,'OutToVecMulti',6, 7);


--difVec3 --- FRAG SHADER
insert into Node values(8,'DifVec3',1,'{0}-{1};');
INSERT into Input values(15,'Vec3_0','',6, 8);
INSERT into Input values(16,'Vec3_1','',6, 8);
INSERT into Output values(7,'OutToVecMulti',6, 8);

--multiplay vec3_scalar  --- FRAG SHADER
insert into Node values(9,'MultiVec3Scalar',1,'{0}*{1};');
INSERT into Input values(17,'scalar_0','',2, 9);
INSERT into Input values(18,'Vec3_1','',6, 9);
INSERT into Output values(8,'OutToVecMulti',6, 9);

--preperNormal -- VERTEX SHADER
insert into Node values(10,'PrepNORMAL',1,'{1} * mat3(transpose(inverse({0})));');
INSERT into Input values(19,'Mat4_0','',16, 10);
INSERT into Input values(20,'Vec3_0','',6, 10);
INSERT into Output values(9,'normalsVec3',6, 10);

--difuse -- frgment shader SHADER
insert into Node values(11,'CalculateDiffuse',1,'max(dot({0},{1}),0.0)*{2};');
INSERT into Input values(21,'norm_vec3','',6, 11);
INSERT into Input values(22,'light_dir_vec3','',6, 11);
INSERT into Input values(23,'light_colorVec3_0','',6, 11);
INSERT into Output values(10,'diffuse_vec3',6, 11);


--Spec -- frgment shader SHADER
insert into Node values(12,'CalculateSpec',1,'pow(max(dot(normalize({0} - {1}),reflect(-{2},{3})), 0.0), 32);');
INSERT into Input values(24,'ViewPos_vec3','',6, 12);
INSERT into Input values(25,'FragPos_vec3','',6, 12);
INSERT into Input values(26,'light_DIR_VEC#','',6, 12);
INSERT into Input values(27,'normVec3_0','',6, 12);
INSERT into Output values(11,'diffuse_vec3',2, 12);

--caluculatte Spectular
insert into Node values(13,'Spectular Calculate',1,'0.5*{0}*{1};');
INSERT into Input values(28,'float','',2, 13);
INSERT into Input values(29,'light_colorVec3_0','',6, 13);
INSERT into Output values(12,'spectular',6, 13);


--Spec -- frgment shader SHADER
insert into Node values(14,'CalculateSpec',1,'vec4(({0}+{1}+{2}),1.0)*{3};');
INSERT into Input values(30,'ambient','',6, 14);
INSERT into Input values(31,'difuse','',6, 14);
INSERT into Input values(32,'spectullar','',6, 14);
INSERT into Input values(33,'inColorVec4_0','',7, 14);
INSERT into Output values(13,'result_vec4',7, 14);



Create Table Output(
                      id int primary key ,
                      Name varchar(255) not null ,
                      DataTypesId int not null ,
                      NodeId int not null ,
                      FOREIGN KEY (DataTypesId) REFERENCES DataTypes(id),
                      FOREIGN KEY (NodeId) REFERENCES Node(id)
);

