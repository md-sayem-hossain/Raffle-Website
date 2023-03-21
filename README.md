# Raffle-Website

# Docker

git pull

docker build -t rafflekings/dev .

docker run --name RaffleKings -d --network=br0.10 --ip 10.10.10.2 -v /mnt/user/appdata/RaffleKings/database:/app/database rafflekings/dev
