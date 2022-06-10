FROM bitnami/postgresql
COPY scripts.sql /docker-entrypoint-initdb.d
EXPOSE 5432
# ENV PGPASSWORD=dbuser123
# CMD [ "psql","-U","dbuser","-d","test","-f","scripts.sql" ]