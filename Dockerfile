# syntax=docker/dockerfile:1

# Stage 1: Base image and directory
FROM node:18-alpine AS base
RUN mkdir -p /app
WORKDIR /app

# Stage 2: Install dependencies
FROM base AS deps
RUN apk add --no-cache libc6-compat
COPY package.json package-lock.json ./
RUN npm ci

# Stage 3: Run as developer
FROM base AS runner
WORKDIR /app
COPY --from=deps app/node_modules ./node_modules

COPY . .
