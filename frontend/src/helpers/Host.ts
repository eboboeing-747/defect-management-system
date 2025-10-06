export class Host {
    private static _host: string = '';

    public static init() {
        const isSecure: Boolean = import.meta.env.IS_SECURE;
        Host._host = isSecure ? import.meta.env.VITE_API_HOST_HTTPS : import.meta.env.VITE_API_HOST_HTTP;
    }

    public static getHost(): string {
        return Host._host;
    }
}
