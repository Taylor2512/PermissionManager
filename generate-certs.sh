#!/bin/bash

# Crear directorio para los certificados
mkdir -p certs

# Crear archivo instances.yml
cat <<EOF > certs/instances.yml
instances:
  - name: es01
    dns:
      - elasticsearch
      - localhost
    ip:
      - 127.0.0.1
  - name: kibana
    dns:
      - kibana
      - localhost
    ip:
      - 127.0.0.1
EOF

# Generar CA
docker run --rm -v $(pwd)/certs:/certs -u 0 docker.elastic.co/elasticsearch/elasticsearch:8.4.0 \
  /bin/bash -c "elasticsearch-certutil ca --pem -out /certs/ca.zip && unzip /certs/ca.zip -d /certs"

# Generar certificados para los nodos
docker run --rm -v $(pwd)/certs:/certs -u 0 docker.elastic.co/elasticsearch/elasticsearch:8.4.0 \
  /bin/bash -c "elasticsearch-certutil cert --pem --in /certs/instances.yml --out /certs/certs.zip --ca-cert /certs/ca/ca.crt --ca-key /certs/ca/ca.key && unzip /certs/certs.zip -d /certs"