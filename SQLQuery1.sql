create table Usuario (
IdUsuario       integer        identity,
Nome            nvarchar(50)    not null,
Email           nvarchar(50)    not null unique,
Senha           nvarchar(50)    not null,
DataCriacao     datetime        not null,
primary key (IdUsuario))
go

