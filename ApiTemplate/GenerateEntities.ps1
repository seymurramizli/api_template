﻿# run script as admin
dotnet-ef dbcontext scaffold "Server=test01,50979\test02;Database=ApiTemplate; Integrated Security=true;  MultipleActiveResultSets=true; MultiSubnetFailover=true" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities  --context-dir Data --context "ApplicationDbContext" --no-onconfiguring --force