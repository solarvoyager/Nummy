# Nummy

> **To use Nummy you need 2 steps**
> 1. Pull nummy image from Docker Hub 
> 2. Run your local instance as container.

```shell
# Step 1 - pull the latest image
docker pull nummy

# or 
# docker image pull nummy:latest

# or specific version
# docker image pull nummy:1.0.0
```

```shell
# note: To see which images are present locally
docker image list
```

```shell
# Step 2 - run container
docker container run nummy
```

```shell
# note: To see which containers are present (running or stopped) locally
docker container list --all
```