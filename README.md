🚧 The website will be live soon. 

This is a project for a car wash business focused on booking. 
1. The visitor first selects a wash option
2. then chooses an available time slots
3. completes the booking by entering their license plate number and email address

 📸 Screenshots:
 
 Service and time slot selection
<img width="1564" height="963" alt="Skärmbild 2025-09-02 174846" src="https://github.com/user-attachments/assets/d9af3891-444c-4fab-a822-bda27d60b4af" />


Booking Form:

This view is shown only after the visitor has selected both a wash option and an available time slot
<img width="916" height="248" alt="Skärmbild 2025-09-02 175041" src="https://github.com/user-attachments/assets/8ebf4687-3e09-4de5-b3ac-98ad97743274" />




Confirmation Modal:

When the booking has been saved, the visitor sees the following modal

 <img width="500" height="429" alt="Skärmbild 2025-09-02 175129" src="https://github.com/user-attachments/assets/c03c2b0c-d60b-4588-b050-25f81e90ab2b" />


 🛠️ Tech Stack
- **Backend**: ASP.NET Core Web API (.NET 8)
- **Frontend**: Blazor server
- **Database**: PostgreSQL


## Running on Kubernetes

You can run this project on a local Kubernetes cluster. The files in `k8s/carwash-k8s.yaml` start four parts in a `carwash` namespace: the **Backend API** (.NET 8), the **Customer UI** and a **PostgreSQL** database.

### What you need

- Docker Desktop with Kubernetes turned on (kubeadm)
- kubectl

### 1. Build the images

docker build -t carwash-backend:local  -f Backend.API/Dockerfile  .
docker build -t carwash-customer:local -f Customer.UI/Dockerfile   .
docker build -t carwash-admin:local    -f Admin.UI/Dockerfile      .


### 2. Deploy to the cluster

 kubectl apply -f k8s/carwash-k8s.yaml

- Check that the pods are running:
  
kubectl get pods -n carwash

### 3. Open the app

- Forward its port:
- 
kubectl port-forward -n carwash service/customer-ui 8080:8080

Now open <http://localhost:8080>.

### How it works

- Each part runs as its own **Deployment**. A **Service** gives it a name, so the UIs can reach the API at `http://backend-api`.
- The database password and connection string are kept in Kubernetes **Secrets**, not in the YAML files.
- A **PersistentVolumeClaim** stores the database data, so it stays even if the pod restarts.
