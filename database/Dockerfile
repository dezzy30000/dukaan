FROM postgres:10.4

ENV PLV8_VERSION=v2.3.5 \
    PLV8_SHASUM="9f037b96c95b3112514cfb4d7e951040cd49d03cba6ae90c55b8d9ffe0c1e182  v2.3.5.tar.gz"

RUN buildDependencies="build-essential \
    ca-certificates \
    wget \
    git \
    python \
    g++ \
    clang \
    postgresql-server-dev-$PG_MAJOR \
    pkg-config \
    libc++-dev \
    libc++abi-dev" \
   && apt-get update \
   && apt-get -y upgrade \
   && apt-get install -y --no-install-recommends ${buildDependencies} \
   && mkdir -p /tmp/plv8 \
   && wget -O /tmp/plv8/${PLV8_VERSION}.tar.gz "https://github.com/plv8/plv8/archive/$PLV8_VERSION.tar.gz" \
   && cd /tmp/plv8 \
   && echo ${PLV8_SHASUM} | sha256sum -c \
   && tar -xzf /tmp/plv8/${PLV8_VERSION}.tar.gz -C /tmp/plv8/ \
   && cd /tmp/plv8/plv8-${PLV8_VERSION#?} \
   && make \
   && make install \
   && cd / \
   && apt-get clean \
   && apt-get remove -y ${buildDependencies} \
   && apt-get autoremove -y \
   && apt-get install -y --no-install-recommends libc++1 \
   && rm -rf /tmp/plv8 /var/lib/apt/lists/*

COPY ./*-*.sql /docker-entrypoint-initdb.d/