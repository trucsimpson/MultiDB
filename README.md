# Master db and multiple replicate db
Dự án này áp dụng mô hình cơ sở dữ liệu master - replicate, với một master db và nhiều replicate db có cấu trúc tương đồng. Số lượng replicate db được xác định dựa vào file appsetting.json. Quá trình ghi dữ liệu sẽ được thực hiện trên cơ sở dữ liệu chính, trong khi việc đọc dữ liệu sẽ được điều hướng đến các replicate db phù hợp, dựa trên thông tin tenant được cung cấp.

```
"ConnectionStrings": {
  "MasterDb": "Data Source=105VNA255;Initial Catalog=UserDb;TrustServerCertificate=True;Integrated Security=True",
  "ReplicaDb_Tenant1": "Data Source=105VNA255;Initial Catalog=UserDbReplica1;TrustServerCertificate=True;Integrated Security=True",
  "ReplicaDb_Tenant2": "Data Source=105VNA255;Initial Catalog=UserDbReplica2;TrustServerCertificate=True;Integrated Security=True",
  "ReplicaDb_Tenant3": "Data Source=105VNA255;Initial Catalog=UserDbReplica3;TrustServerCertificate=True;Integrated Security=True",
  "ReplicaDb_Tenant4": "Data Source=105VNA255;Initial Catalog=UserDbReplica4;TrustServerCertificate=True;Integrated Security=True",
}
```
TenantId = Tenant1, Tenant2, Tenant3, Tenant4... TenantId trong project sẽ dựa vào cấu trúc như trên

## Add Migration
```
Add-Migration MigrationName -OutputDir "Data/Migrations" -Context ApplicationDbContext
```

## Update Migration cho master DB
```
Update-Database -Context ApplicationDbContext
```

## Update Migration cho replicate DB
```
Update-Database -Args "Tenant1" -Context ApplicationDbContext
```
Trong đó "Tenant1" là TenantId truyền vào.

Khi add Migration rồi có thể không cần chạy lệnh update Migration. Khi start lên tự động update Migration cho từng database
