FROM node:20 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

FROM nginx:alpine

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /tmp/dist

RUN rm -rf /usr/share/nginx/html/* && \
    cp -r $(dirname $(find /tmp/dist -name index.html | head -n 1))/* /usr/share/nginx/html/

EXPOSE 80
