﻿@rem 生成客户端和服务器端存根

setlocal

@rem 进入当前目录

cd /d %~dp0

set TOOLS_PATH=C:\Users\wangz\.nuget\packages\grpc.tools\2.24.0\tools\windows_x64

%TOOLS_PATH%\protoc.exe ^
--proto_path C:\Users\wangz\Desktop\Demo\GRpc\GrpcGateway\Grpc.Infrastructure ^
--csharp_out=C:\Users\wangz\Desktop\Demo\GRpc\GrpcGateway\Grpc.Infrastructure\CSharp ^
--grpc_out=C:\Users\wangz\Desktop\Demo\GRpc\GrpcGateway\Grpc.Infrastructure\CSharp  ^
--plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe ^
C:\Users\wangz\Desktop\Demo\GRpc\GrpcGateway\Grpc.Infrastructure\executeGrpc.proto

endlocal
timeout 1000