let $tooltips = $("[data-bs-toggle='tooltip']");

$tooltips.each((index, tooltip) => new bootstrap.Tooltip(tooltip));