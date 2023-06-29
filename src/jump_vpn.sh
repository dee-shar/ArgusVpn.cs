#!/bin/bash

api="https://www.jwckk.top/api"
user_agent="Dart/2.19 (dart:io)"

function login() {
	response=$(curl --request POST \
		--url "$api/login/temp" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--data '{
			"deviceId": "'$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 16 | head -n 1)'",
			"deviceName": "RMX3551"
		}')
	if [ -n $(jq -r ".data.user.token" <<< "$response") ]; then
		token=$(jq -r ".data.user.token" <<< "$response")
	fi
	echo $response
}

function get_servers() {
	curl --request GET \
		--url "$api/fetch/client/node/v2/subscribe?token=$token" \
		--user-agent "$user_agent"
}

function get_account_info() {
	curl --request POST \
		--url "$api/fetch/userInfo" \
		--user-agent "$user_agent" \
		--header "authorization: $token"
}

function check_ip() {
	curl --request GET \
		--url "$api/fetch/user/checkIp" \
		--user-agent "$user_agent" \
		--header "authorization: $token"
}
