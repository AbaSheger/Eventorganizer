/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        "primary-blue": "#1E88E5",
        "primary-dark": "#1565C0",
        "primary-light": "#64B5F6",
        "positive-green": "#4CAF50",
        "negative-red": "#F44336",
        "neutral-gray": "#757575",
        "bg-primary": "#FFFFFF",
        "bg-secondary": "#F5F7FA",
        "bg-tertiary": "#EAEEF3",
        "text-primary": "#212121",
        "text-secondary": "#616161",
        "text-tertiary": "#9E9E9E",
        "border-light": "#E0E0E0",
        "border-medium": "#BDBDBD",
      },
      fontFamily: {
        sans: [
          "Inter",
          "-apple-system",
          "BlinkMacSystemFont",
          "Segoe UI",
          "Roboto",
          "Oxygen",
          "Ubuntu",
          "Cantarell",
          "sans-serif",
        ],
      },
      boxShadow: {
        sm: "0 1px 2px 0 rgba(0, 0, 0, 0.05)",
        md: "0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)",
        lg: "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)",
      },
    },
  },
  plugins: [],
}