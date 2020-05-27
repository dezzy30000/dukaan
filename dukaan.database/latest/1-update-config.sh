#!/usr/bin/env bash

# Allow connections from anywhere.
sed -i -e"s/^#listen_addresses =.*$/listen_addresses = '*'/" /var/lib/postgresql/data/postgresql.conf