FROM postgres:11.4

ENV PLV8_VERSION=v2.3.12 \
    PLV8_SHASUM="0bd5cbd8e6123d14451780240972289dcf53cf8c1ffa227aa9f34c296cd641d8  v2.3.12.tar.gz"

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

COPY database/*-*.sql database/*-*.sh /docker-entrypoint-initdb.d/