import secrets

ENV_FILENAME = ".env"
WRITE_MODE = 'w'

LINEBREAK = '\n'
EMPTY_STRING = ""

def main():
    key = get_values()

    if input_is_valid(key):
        lines = [
            f"API_KEY={key}"
        ]

        with open(ENV_FILENAME, WRITE_MODE) as file:
            for line in lines:
                file.write(line)
                file.write(LINEBREAK)
    else:
        print(f"INPUT INVALID; `{ENV_FILENAME}` NOT CREATED.")

def get_values():
    key = input("Enter API Key: ")
    if key == EMPTY_STRING:
        key = secrets.token_hex(32)

    return key

def input_is_valid(key):
    return len(key) == 64

if __name__ == "__main__":
    main()
